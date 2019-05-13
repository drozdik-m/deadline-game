using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectTest : MonoBehaviour
{
    public SoundEffectController controllerFoot;
    public SoundEffectController controllerClick;
    public SoundEffectController controllerVoice;

    public void PlayFoot()
    {
        controllerFoot.PlayLoopSound (0.3f);
    }

    public void StopFoot()
    {
        controllerFoot.StopLoopSound ();
    }

    public void PlayClick()
    {
        controllerClick.PlaySound (transform);
    }

    public void PlayVoice()
    {
        controllerVoice.PlaySound (controllerVoice.transform);
    }
}
