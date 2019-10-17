using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collection of build stages
/// </summary>
public class BuildStageCollection : MonoBehaviour
{
    private int nextIndex;

    /// <summary>
    /// stages of the buildable object
    /// </summary>
    public BuildStage[] stages = new BuildStage[0];

    /// <summary>
    /// Current (active) stage
    /// </summary>
    private BuildStage currentBuildStage;

    /// <summary>
    /// Initializes the build stage
    /// </summary>
    public void Init()
    {
        // do we have any stages connected?
        if (stages.Length == 0)
            throw new ArgumentException("BuildStageCollection must have atleast one stage");

        nextIndex = 0;
    }

    /// <summary>
    /// Get next stage from the queue
    /// </summary>
    /// <returns>Next stage from the queue</returns>
    public BuildStage GetNext()
    {
        if (nextIndex < stages.Length)
        {
            BuildStage nextBuildStage = stages[nextIndex];
            nextIndex++;
            return nextBuildStage;
        }
        else
            return null;
    }
}
