using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StageFadeAbstract : Stage
{
    public FaderController Fader = null;

    private void Start()
    {
        if (Fader == null)
            Fader = GameObject.FindGameObjectWithTag("Fader").GetComponent<FaderController>();
    }

    public override bool ReadyForNextStage()
    {
        if (Fader == null)
            return false;
        return !Fader.IsFading();
    }
}
