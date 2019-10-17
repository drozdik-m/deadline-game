using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class StyleblitDriver : MonoBehaviour
{
    [DllImport("/Assets/alglibnet2.dll")]
    private static extern void kdtreebuild(double[,] xy, int nx, int ny, int normtypeout, out alglib.kdtree kdt, alglib.xparams _params);
    [DllImport("/Assets/alglibnet2.dll")]
    private static extern int kdtreequeryknn(alglib.kdtree kdt, double[] x, int k, alglib.xparams _params);

    const int TEXTURE_DIM = 512;
    const int JITTER_SIZE = 256;

    public Texture m_NormalSource;

    public bool generateLUT = false;

    private Renderer m_Renderer;
    private Texture2D m_Jitter;

    private bool skipFrame = false;


    private void SaveTexture(string path, Texture2D tex)
    {
        byte[] bytes = tex.EncodeToPNG();
        Debug.Log("Texture saved in " + path);
        File.WriteAllBytes(path, bytes);
    }

    private void RenderJitterTexture(Texture2D nt)
    {
        //Random.InitState(Time.frameCount);
        for (int y = 0; y < JITTER_SIZE; y++)
        {
            for (int x = 0; x < JITTER_SIZE; x++)
            {
                Color c = new Color(Random.value, Random.value, Random.value, Random.value);
                nt.SetPixel(x, y, c);
            }
        }
        nt.Apply();
    }

    private void CreateLUT(Texture2D source, ref Texture2D lut)
    {
        Texture2D translator = new Texture2D(source.width, source.height, TextureFormat.ARGB32, false);
        double[,] points = new double[source.width * source.height, 2];
        int i = 0;
        for (int y = 0; y < source.height; y++)
        {
            for (int x = 0; x < source.width; x++)
            {
                Color c = source.GetPixel(x, y);
                points[i, 0] = c.r;
                points[i, 1] = c.g;
                translator.SetPixel((int)(points[i, 0] * source.width), (int)(points[i, 1] * source.height), new Color(x / (float)source.width, y / (float)source.height, 0, 1));
                i++;
            }
        }

        translator.Apply();

        alglib.kdtree kdt;
        alglib.kdtreebuild(points, 2, 0, 2, out kdt);
        double[,] result = new double[0, 0];
        
        for (int y = 0; y < source.height; y++)
        {
            for (int x = 0; x < source.width; x++)
            {
                double[] q = new double[] { x / (double)source.width, y / (double)source.height };
                alglib.kdtreequeryknn(kdt, q, 1);
                alglib.kdtreequeryresultsx(kdt, ref result);

                double[] nn = { result[0, 0], result[0, 1] };
                Color tint = translator.GetPixel((int)(nn[0] * source.width), (int)(nn[1] * source.height));
                
                lut.SetPixel(x, y, tint);
            }
        }
        lut.Apply();
    }

    void Update()
    {
        if (!skipFrame)
        {
            m_Jitter = Texture2D.blackTexture;
            m_Jitter.Resize(JITTER_SIZE, JITTER_SIZE);

            RenderJitterTexture(m_Jitter);
            m_Renderer.material.SetTexture("noiseTexture", m_Jitter);

            skipFrame = true;
        }
        else
        {
            skipFrame = false;
        }
    }

    void Start()
    {
        Application.targetFrameRate = 24;
        QualitySettings.vSyncCount = 0;

        m_Renderer = GetComponent<Renderer>();
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        m_Jitter = new Texture2D(JITTER_SIZE, JITTER_SIZE, TextureFormat.RGBAFloat, false);

        if (generateLUT)
        {
            Texture2D lut = new Texture2D(m_NormalSource.width, m_NormalSource.height, TextureFormat.RGBAFloat, false);

            CreateLUT((Texture2D)m_NormalSource, ref lut);
            m_Renderer.material.SetTexture("normalToSourceLUT", lut);

            string pathLUT = Application.dataPath + "/" + mesh.name + "lut.png";
            SaveTexture(pathLUT, lut);
        }
    }
}
