using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageReactionCondition : Stage
{
    /// <summary>
    /// Condition that will be edited
    /// </summary>
    public Condition Condition;

    public override bool ReadyForNextStage()
    {
        if (Condition == null)
            return false;
        return Condition.satisfied;
    }

    public override void StageEnd()
    {
        
    }

    public override void StageLoad()
    {
        
    }

    public override void StageFixedUpdate()
    {
        
    }

    public override void StageUpdate()
    {
        
    }
}
