using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Condition reaction is used when we need reaction that will set satisfied property to some condition.
/// </summary>
public class ConditionReaction : Reaction
{
    /// <summary>
    /// condition that will be edited
    /// </summary>
    public Condition condition;

    /// <summary>
    /// new satisfied value for condition
    /// </summary>
    public bool satisfied;

    protected override void ImmediateReaction()
    {
        condition.satisfied = satisfied;
    }
}
