using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stage that performs FadeInOut
/// </summary>
public class StageFadeInOut : StageFadeAbstract
{
    /// <summary>
    /// Fade in color
    /// </summary>
    public Color FadeInColor = Color.black;

    /// <summary>
    /// Fade in length
    /// </summary>
    public float FadeInLength = 3;

    /// <summary>
    /// Wait for fade out
    /// </summary>
    public float FadeInDelay = 1;

    /// <summary>
    /// Fade out color
    /// </summary>
    public Color FadeOutColor = Color.black;

    /// <summary>
    /// Fade out length
    /// </summary>
    public float FadeOutLength = 3;

    public override void StageEnd()
    {
        
    }

    public override void StageFixedUpdate()
    {
        
    }

    public override void StageLoad()
    {
        Fader.FadeInOut(FadeInLength, FadeInColor, FadeOutLength, FadeOutColor, FadeInDelay);
    }

    public override void StageUpdate()
    {
        
    }
}
