using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicChangeRequest
{
    public BackgroundMusicTheme newTheme;
    public float transitionLength;

    public BackgroundMusicChangeRequest(BackgroundMusicTheme newTheme, float transitionLength)
    {
        this.newTheme = newTheme;
        this.transitionLength = transitionLength;
    }
}
