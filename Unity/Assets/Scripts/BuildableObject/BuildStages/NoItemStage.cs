using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoItemStage : BuildStage
{
    public override bool ConditionsSatisfied()
    {
        Debug.Log("NoItemStage in ConditionsSatisfied");
        return true;
    }
}
