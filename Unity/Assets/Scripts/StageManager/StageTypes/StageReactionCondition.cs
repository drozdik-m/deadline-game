using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageReactionCondition : Stage
{
    /// <summary>
    /// Condition that will be edited
    /// </summary>
    public Condition Condition;

    /// <summary>
    /// New satisfied value for condition
    /// </summary>
    public bool Satisfied;

    public override bool ReadyForNextStage()
    {
        return true;
    }

    public override void StageEnd()
    {
        
    }

    public override void StageLoad()
    {
        Condition.satisfied = Satisfied;
    }

    public override void StageFixedUpdate()
    {
        
    }

    public override void StageUpdate()
    {
        
    }
}
