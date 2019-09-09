using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Represents build stage for what player does not need anything
/// </summary>
public class NoItemStage : BuildStage
{
    public override Type UIBuildableStageType
    {
        get
        {
            return typeof(NoItemUI);
        }
    }

    /// <summary>
    /// Conditions are always satisfied
    /// </summary>
    /// <returns></returns>
    public override bool ConditionsSatisfied()
    {
        return true;
    }
}
