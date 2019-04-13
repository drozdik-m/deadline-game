using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stage that performs fading in/out
/// </summary>
public class StageFade : StageFadeAbstract
{
    /// <summary>
    /// Fade color
    /// </summary>
    public Color FadeColor = Color.black;

    /// <summary>
    /// Fade length
    /// </summary>
    public float FadeLength = 3;
    
    /// <summary>
    /// Fade in/out
    /// </summary>
    public StageFadeOption FadeStyle;


    public override void StageEnd()
    {
        
    }

    public override void StageFixedUpdate()
    {
        
    }

    public override void StageLoad()
    {
        if (FadeStyle == StageFadeOption.FadeIn)
            Fader.FadeIn(FadeLength, FadeColor);
        else if (FadeStyle == StageFadeOption.FadeOut)
            Fader.FadeOut(FadeLength, FadeColor); 
    }

    public override void StageUpdate()
    {
        
    }
}

public enum StageFadeOption
{
    FadeIn,
    FadeOut
}
