using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    //Theme music files
    public AudioClip SpookyMusic;
    public AudioClip ScaryMusic;
    public AudioClip RegularMusic;
    public AudioClip HappyMusic;

    //Players
    private AudioSource audioSource;
    private FadingAudioSource fadingController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fadingController = GetComponent<FadingAudioSource>();
    }

    public void ChangeTheme(BackgroundMusicTheme newTheme, float transitionSpeed = 0.3f)
    {
        fadingController.FadeSpeed = transitionSpeed;
        if (newTheme == BackgroundMusicTheme.Happy)
            fadingController.Fade(HappyMusic, 1, true);
        if (newTheme == BackgroundMusicTheme.NoMusic)
            fadingController.Fade(null, 1, true);
        if (newTheme == BackgroundMusicTheme.Regular)
            fadingController.Fade(RegularMusic, 1, true);
        if (newTheme == BackgroundMusicTheme.Scary)
            fadingController.Fade(ScaryMusic, 1, true);
        if (newTheme == BackgroundMusicTheme.Spooky)
            fadingController.Fade(SpookyMusic, 1, true);
    }
}


public enum BackgroundMusicTheme{
    Spooky,
    Scary,
    Regular,
    Happy,
    NoMusic
}