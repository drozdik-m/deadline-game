using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StageFadeAbstract : Stage
{
    [Tooltip("Optional if GameObject with \"Fader\" tag is present")]
    public FaderController OverrideFader;
    protected FaderController Fader;

    private void Start()
    {
        if (OverrideFader == null)
            Fader = GameObject.FindGameObjectWithTag("Fader").GetComponent<FaderController>();
        else
            Fader = OverrideFader;
    }

    public override bool ReadyForNextStage()
    {
        if (Fader == null)
            return false;
        return !Fader.IsFading();
    }
}
