using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionReaction : Reaction
{
    public Condition condition;
    public bool satisfied;

    protected override void ImmediateReaction()
    {
        condition.satisfied = satisfied;
    }
}
