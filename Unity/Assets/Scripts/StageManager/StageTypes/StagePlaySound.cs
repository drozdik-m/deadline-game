using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePlaySound : Stage
{
    /// <summary>
    /// Condition that will be edited
    /// </summary>
    public SoundEffectController SoundEffectController;

    public override bool ReadyForNextStage()
    {
        return true;
    }

    public override void StageEnd()
    {
        
    }

    public override void StageLoad()
    {
        SoundEffectController.PlaySound();
    }

    public override void StageFixedUpdate()
    {
        
    }

    public override void StageUpdate()
    {
        
    }
}
