using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StageFadeAbstract : Stage
{
    [Tooltip("Optional if GameObject with \"Fader\" tag is present")]
    public FaderController Fader;

    private void Start()
    {
        if (Fader == null)
            Fader = GameObject.FindGameObjectWithTag("Fader").GetComponent<FaderController>();
    }

    public override bool ReadyForNextStage()
    {
        return !Fader.IsFading();
    }
}
