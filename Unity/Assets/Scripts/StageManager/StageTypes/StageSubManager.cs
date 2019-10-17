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
    public StageManager StageManager = null;

    public override bool ReadyForNextStage()
    {
        if (StageManager == null)
            return false;
        return StageManager.IsFinished();
    }

    public override void StageEnd()
    {
        
    }

    public override void StageFixedUpdate()
    {
        
    }

    public override void StageLoad()
    {
        if (StageManager == null)
            StageManager = GetComponent<StageManager>();
        StageManager.InitiateStages();
    }

    public override void StageUpdate()
    {
        
    }
}
