using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic_testEnviroment : MonoBehaviour
{
    public void PlaySpookyMusic()
    {
        GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<BackgroundMusic>().ChangeTheme(BackgroundMusicTheme.Spooky);
    }

    public void PlayNoMusic()
    {
        GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<BackgroundMusic>().ChangeTheme(BackgroundMusicTheme.NoMusic);
    }

    public void PlayRegularMusic()
    {
        GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<BackgroundMusic>().ChangeTheme(BackgroundMusicTheme.Regular);
    }
}
