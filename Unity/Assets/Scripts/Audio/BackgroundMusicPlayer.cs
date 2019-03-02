using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    //EVENTS
    public event BackgroundMusicChangeEvent OnMusicPlayAnimationStart;
    public event BackgroundMusicChangeEvent OnMusicPlayAnimationEnd;
    public event BackgroundMusicChangeEvent OnMusicMuteAnimationStart;
    public event BackgroundMusicChangeEvent OnMusicMuteAnimationEnd;

    //Audio source
    private AudioSource audioSource;

    //Animation
    private Animator animationController;

    //START
    private void Start()
    {
        //Init components
        audioSource = GetComponent<AudioSource>();
        animationController = GetComponent<Animator>();

        animationController.SetBool("Play", false);
    }

    public void Play(AudioClip music)
    {
        audioSource.clip = music;
        audioSource.Play();
        animationController.SetBool("Play", true);
        OnMusicPlayAnimationStart?.Invoke(this, new BackgroundMusicChangeEventArgs());
    }

    public void Stop()
    {
        animationController.SetBool("Play", false);
        audioSource.clip = null;
        OnMusicMuteAnimationStart?.Invoke(this, new BackgroundMusicChangeEventArgs());
    }

    public void AnimationObserver(string status)
    {
        if (status == "FadeOutEnd")
            OnMusicMuteAnimationEnd?.Invoke(this, new BackgroundMusicChangeEventArgs());
        else if (status == "FadeInEnd")
            OnMusicPlayAnimationEnd?.Invoke(this, new BackgroundMusicChangeEventArgs());
    }
}


