using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stage that runs sub stage manager
/// </summary>
public class StageConditionalSubManager : StageSubManager
{
    /// <summary>
    /// Condition to check
    /// </summary>
    public Condition Condition = null;

    private bool originalCondition;

    public override bool ReadyForNextStage()
    {
        if (!originalCondition)
            return true;
        if (StageManager == null)
            return false;
        return StageManager.IsFinished();
    }

    public override void StageLoad()
    {
        if (Condition != null && (originalCondition = Condition.satisfied))
        {
            if (StageManager == null)
                StageManager = GetComponent<StageManager>();
            StageManager.InitiateStages();
        }
    }
}
