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

    public override void StageLoad()
    {
        if (Condition != null && Condition.satisfied)
        {
            if (StageManager == null)
                StageManager = GetComponent<StageManager>();
            StageManager.InitiateStages();
        }
    }
}
