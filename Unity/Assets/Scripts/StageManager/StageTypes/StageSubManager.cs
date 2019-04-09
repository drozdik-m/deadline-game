using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stage that runs sub stage manager
/// </summary>
public class StageSubManager : Stage
{
    /// <summary>
    /// Stage mamager to run
    /// </summary>
    public StageManager stageManager;

    public override bool ReadyForNextStage()
    {
        return stageManager.IsFinished();
    }

    public override void StageEnd()
    {
        
    }

    public override void StageFixedUpdate()
    {
        
    }

    public override void StageLoad()
    {
        stageManager.InitiateStages();
    }

    public override void StageUpdate()
    {
        
    }
}
