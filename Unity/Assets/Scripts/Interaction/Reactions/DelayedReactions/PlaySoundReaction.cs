using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundReaction : DelayedReaction
{
    public SoundEffectController soundEffectController;

    protected override void ImmediateReaction()
    {
        soundEffectController.PlaySound();
    }
}
