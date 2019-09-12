using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Initial build stage. The one that sets up the game objects to be visible or hidden
/// on the beginning
/// </summary>
public class InitialStage : BuildStage
{
    public override Type UIBuildableStageType
    {
        get
        {
            return typeof(InitialStage);
        }
    }

    /// <summary>
    /// We are already in the stage, does not matter what we actually return
    /// </summary>
    /// <returns></returns>
    public override bool ConditionsSatisfied()
    {
        return true;
    }
}
