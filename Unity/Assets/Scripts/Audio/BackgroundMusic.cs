using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Background music handler
/// </summary>
public class BackgroundMusic : MonoBehaviour
{
   
    //Theme music files
    /// <summary>
    /// Spooky music clip
    /// </summary>
    public AudioClip SpookyMusic;

    /// <summary>
    /// Background music for day1
    /// </summary>
    public AudioClip Day1Music;


    /// <summary>
    /// Dark/scary music for night1
    /// </summary>
    public AudioClip DarkPiano;

    /// <summary>
    /// Scary music clip
    /// </summary>
    public AudioClip ScaryMusic;

    /// <summary>
    /// Regular music clip
    /// </summary>
    public AudioClip RegularMusic;

    /// <summary>
    /// Happy music clip
    /// </summary>
    public AudioClip HappyMusic;

    /// <summary>
    /// Elevator music clip
    /// </summary>
    public AudioClip ElevatorMusic;

    //Players
    private AudioSource audioSource;
    private FadingAudioSource fadingController;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        fadingController = GetComponent<FadingAudioSource>();
    }

    /// <summary>
    /// Change music background theme
    /// </summary>
    /// <param name="newTheme">New theme, music will transition (enum)</param>
    /// <param name="transitionSpeed">Speed parameter (lower = slower), default is 0.3f</param>
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
        if (newTheme == BackgroundMusicTheme.ElevatorMusic)
            fadingController.Fade(ElevatorMusic, 1, true);
        if (newTheme == BackgroundMusicTheme.Day1Bg)
            fadingController.Fade(Day1Music, 1, true);
        if (newTheme == BackgroundMusicTheme.DarkPiano)
            fadingController.Fade(DarkPiano, 1, true);
    }
}


public enum BackgroundMusicTheme{
    Spooky,
    Scary,
    Regular,
    Happy,
    NoMusic,
    ElevatorMusic,
    Day1Bg,
    DarkPiano
}
