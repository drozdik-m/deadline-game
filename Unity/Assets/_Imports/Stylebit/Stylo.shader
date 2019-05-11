Shader "Stylo"
{
	Properties
	{
		_threshold("Chunk threshold", Range(0.01,0.2)) = 0.07
		_votespan("Voting span", Range(1,5)) = 2
		_splatsize("Max splat size", Range(15,1)) = 9
		[HideInInspector] _koef("Koef", Float) = 10

		normalToSourceLUT("Normal LUT texture", 2D) = ""
		sourceNormals("Source normals texture", 2D) = ""
		sourceStyle("Style texture", 2D) = ""
		noiseTexture("Noise texture", 2D) = "white"

	}

		
	SubShader
	{
		Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
		Lighting Off

		Pass
		{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				float3 normal = COMPUTE_VIEW_NORMAL;
				normal = 0.f - normal;

				o.color.rgb = normal * 0.5f + 0.5f;
				o.color.w = 1.f;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return i.color;
			}
			ENDCG
		}

		GrabPass
		{
			"_NormalTexture"
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 grabPos : TEXCOORD1;
				float4 screenPos : TEXCOORD2;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				o.uv = v.uv;

				o.grabPos = ComputeGrabScreenPos(o.vertex);
				o.uv = o.grabPos.xy / o.grabPos.w;

				o.screenPos = ComputeScreenPos(o.vertex);
				return o;
			}


			sampler2D_float normalToSourceLUT;
			float4 normalToSourceLUT_TexelSize;
			sampler2D_float sourceNormals;
			float4 sourceNormals_TexelSize;
			sampler2D_float noiseTexture;
			float4 noiseTexture_TexelSize;
			sampler2D_float sourceStyle;
			float4 sourceStyle_TexelSize;

			sampler2D_float _NormalTexture;
			float4 _NormalTexture_TexelSize;

			float _threshold;
			float _koef;
			int _splatsize;

			float2 proj(float3 n)
			{
				return float2(n.x / n.z, n.y / n.z);
			}

			float2 proj(float2 n, float2 w)
			{
				return float2(n.x / w.x, n.y / w.y);
			}

			float2 lookupCoord(float2 pxc)
			{
				float4 pattern = tex2Dlod(_NormalTexture, 
					float4(proj(pxc, _NormalTexture_TexelSize.zw), 0.f, 0.f));

				return tex2Dlod(normalToSourceLUT, float4(pattern.rg, 0.f, 0.f)).rg;
			}

			float countGuideError(float2 sourceCoords, float2 targetCoords)
			{
				float4 diff = abs(tex2Dlod(_NormalTexture, float4(targetCoords, 0.f, 0.f)) -
					tex2Dlod(sourceNormals, float4(sourceCoords, 0.f, 0.f)));
				return diff.x + diff.y + diff.z;
			}

			float4 decon(float2 coords)
			{
				float konf = _koef;

				float2 sect;
				coords.x = modf(coords.x*konf, sect.x);
				coords.y = modf(coords.y*konf, sect.y);

				sect /= konf;

				return float4(coords, sect);

			}

			float2 findSeed(float2 pxc, float spacing, int x, int y)
			{
				float2 coords = floor(pxc / spacing);
				float2 jitter = tex2Dlod(noiseTexture, 
					float4(proj(coords, noiseTexture_TexelSize.zw), 0.f, 0.f)).rg;
				jitter *= 4;
				return (coords + jitter + float2(x, y)) * spacing;
			}

			float2 findNearestSeed(float2 pxc, float spacing, int x, int y)
			{
				float bestD = 3.402823466e+38f;
				float2 bestPxc = float2(0.f, 0.f);

				for (int i = -1; i <= 1; i++)
				{
					for (int jitter = -1; jitter <= 1; jitter++)
					{
						float2 p = findSeed((pxc + float2(i, jitter) * spacing), spacing, x, y);
						float d = distance(p, pxc);
						if (bestD > d)
						{
							bestPxc = p;
							bestD = d;
						}
					}
				}

				return bestPxc;
			}



			void findBestCoords(float2 pxc, float2 origSeedPxc, int l, float thresh,
				inout float2 bestCoords, inout float bestError, inout int bestL,
				inout float2 bestSeed)
			{
				float2 seedPxc = lookupCoord(origSeedPxc) * sourceNormals_TexelSize.zw;
				
				float2 sourcePxc = seedPxc + (pxc - origSeedPxc);
				float2 sourceCoords = proj(sourcePxc, sourceNormals_TexelSize.zw);

				if ((distance(pxc, origSeedPxc) < l / 2.f) && 
					all(sourcePxc >= float2(0.f, 0.f)) &&
					all(sourcePxc < sourceNormals_TexelSize.zw))
				{
					float error = countGuideError(sourceCoords, proj(pxc.xy, _NormalTexture_TexelSize.zw));

					if (error < thresh && error < bestError)
					{
						bestL = l;
						bestError = error;

						bestCoords = sourceCoords;
						bestSeed = proj(seedPxc, sourceNormals_TexelSize.zw);
					}
				}
			}

			float4 blit(v2f input)
			{
				int levels = _splatsize;
				float thresh = _threshold;
				float4 color = float4(0.f, 0.f, 0.f, 1.f);

				float bestError = 3.402823466e+38f;
				float bestL = -1;
				float2 pxc = proj(input.screenPos.xyw);
				pxc *= _ScreenParams.xy;

				float2 bestCoords = lookupCoord(pxc);

				float2 bestSeed = float2(0.f, 0.f);

				float2 nearSeed;

				for (int i = levels; i > 0; i--)
				{
					int l = pow(2, i);

					if (l > bestL)
					{
						for (int x = -1; x <= 1; x++)
						{
							for (int y = -1; y <= 1; y++)
							{
								nearSeed = findNearestSeed(pxc, l, x, y);
								findBestCoords(pxc, nearSeed, l, thresh,
									bestCoords, bestError, bestL, bestSeed);
							}
						}
					}
				}

				return decon(bestCoords.xy);

			}


			fixed4 frag(v2f i) : SV_Target
			{
				return blit(i);
			}
			ENDCG
		}

		GrabPass
		{
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 grabPos : TEXCOORD1;
				float4 vertex : SV_POSITION;
				float4 screenPos : TEXCOORD2;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				
				o.grabPos = ComputeGrabScreenPos(o.vertex);
				o.screenPos = ComputeScreenPos(o.vertex);
				return o;
			}

			sampler2D_float _GrabTexture;
			float4 _GrabTexture_TexelSize;
			sampler2D_float sourceStyle;
			float4 sourceStyle_TexelSize;

			float _votespan;
			float _koef;

			float2 recon(float4 pattern)
			{
				float konf = _koef;

				pattern.zw *= konf;
				pattern.x += pattern.z;
				pattern.y += pattern.w;
				pattern.xy /= konf;

				return pattern.xy;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float2 pixelSize = float2(_GrabTexture_TexelSize.x, _GrabTexture_TexelSize.y);
				float4 color = float4(0.f, 0.f, 0.f, 1.f);
			
				float2 pos = i.screenPos.xy / i.screenPos.w;
				pos *= _ScreenParams.xy;

				int w = 0;
				int vic = _votespan;

				for (int x = -vic; x <= vic; x++)
				{
					for (int y = -vic; y <= vic; y++)
					{
						float2 coords = recon(tex2D(_GrabTexture, float2(pos.x + x, pos.y + y) / _GrabTexture_TexelSize.zw));
						coords *= sourceStyle_TexelSize.zw;
						coords -= float2(x, y);
						color += tex2D(sourceStyle, coords.xy / sourceStyle_TexelSize.zw);
						w++;
					}
				}

				float4 smooth = color / w;

				return smooth;

			}
			ENDCG
		}
	}
}
