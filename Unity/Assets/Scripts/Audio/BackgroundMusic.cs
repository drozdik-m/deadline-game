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

    //Change queue
    private Queue<BackgroundMusicChangeRequest> changeQueue = new Queue<BackgroundMusicChangeRequest>();

    //Backgroud music player PREFAB
    public GameObject BackgroundMusicPlayer;

    //Change lock
    private bool locked = false;

    //Players
    private GameObject backgroudMusicPlayerObject;
    private BackgroundMusicPlayer backgroudMusicPlayer;


    void Start()
    {
        //Instantiate sound players
        backgroudMusicPlayerObject = (GameObject)Instantiate(BackgroundMusicPlayer);
        backgroudMusicPlayer = backgroudMusicPlayerObject.GetComponent<BackgroundMusicPlayer>();

        //Add lock changes and invokes
        backgroudMusicPlayer.OnMusicPlayAnimationEnd += UnlockEvent;

    }

    private void UnlockEvent(BackgroundMusicPlayer caller, BackgroundMusicChangeEventArgs args)
    {
        locked = false;
        NextChange();
    }

    public void ChangeTheme(BackgroundMusicTheme newTheme, float transitionLength = 5f)
    {
        changeQueue.Enqueue(new BackgroundMusicChangeRequest(newTheme, transitionLength));

        if (!locked)
            NextChange();
    }

    private void NextChange()
    {
        //Is queue empty?
        if (changeQueue.Count == 0)
            return;

        //Lock&Go
        locked = true;

        var change = changeQueue.Dequeue();

        if (change.newTheme == BackgroundMusicTheme.Happy)
            backgroudMusicPlayer.Play(HappyMusic);
        if (change.newTheme == BackgroundMusicTheme.NoMusic)
        {
            backgroudMusicPlayer.Stop();
            locked = false;
        }
        if (change.newTheme == BackgroundMusicTheme.Regular)
            backgroudMusicPlayer.Play(RegularMusic);
        if (change.newTheme == BackgroundMusicTheme.Scary)
            backgroudMusicPlayer.Play(ScaryMusic);
        if (change.newTheme == BackgroundMusicTheme.Spooky)
            backgroudMusicPlayer.Play(SpookyMusic);
    }


}


public enum BackgroundMusicTheme{
    Spooky,
    Scary,
    Regular,
    Happy,
    NoMusic
}