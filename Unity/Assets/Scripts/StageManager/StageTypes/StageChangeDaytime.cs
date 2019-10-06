using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stage that performs fading in/out
/// </summary>
public class StageChangeDaytime : Stage
{
    
    public SkyboxController.Time TargetTime;

    public override bool ReadyForNextStage()
    {
        return true;
    }

    public override void StageEnd()
    {
        
    }

    public override void StageFixedUpdate()
    {
        
    }

    public override void StageLoad()
    {
        FindObjectOfType<SkyboxController>().ChangeTime(TargetTime);
    }

    public override void StageUpdate()
    {
        
    }
}
