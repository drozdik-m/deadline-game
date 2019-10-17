using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectTest : MonoBehaviour
{
    public SoundEffectController controllerClick;
    public SoundEffectController controllerVoice;

    public void PlayClick()
    {
        controllerClick.PlaySound (transform);
    }

    public void PlayVoice()
    {
        controllerVoice.PlaySound (controllerVoice.transform);
    }
}
