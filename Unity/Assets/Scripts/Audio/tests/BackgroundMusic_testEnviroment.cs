using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic_testEnviroment : MonoBehaviour
{
    /// <summary>
    /// Testing method for spooky music
    /// </summary>
    public void PlaySpookyMusic()
    {
        GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<BackgroundMusic>().ChangeTheme(BackgroundMusicTheme.Spooky);
    }

    /// <summary>
    /// Testing method for no music
    /// </summary>
    public void PlayNoMusic()
    {
        GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<BackgroundMusic>().ChangeTheme(BackgroundMusicTheme.NoMusic);
    }

    /// <summary>
    /// Testing method for regular music
    /// </summary>
    public void PlayRegularMusic()
    {
        GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<BackgroundMusic>().ChangeTheme(BackgroundMusicTheme.Regular);
    }
}
