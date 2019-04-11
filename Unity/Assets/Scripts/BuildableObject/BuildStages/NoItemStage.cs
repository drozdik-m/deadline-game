using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents build stage for what player does not need anything
/// </summary>
public class NoItemStage : BuildStage
{
    /// <summary>
    /// Conditions are always satisfied
    /// </summary>
    /// <returns></returns>
    public override bool ConditionsSatisfied()
    {
        return true;
    }
}
