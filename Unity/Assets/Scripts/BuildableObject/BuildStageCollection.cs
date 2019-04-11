using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collection of build stages
/// </summary>
public class BuildStageCollection : MonoBehaviour
{
    private int remaining;
    private int nextIndex;

    /// <summary>
    /// stages of the buildable object
    /// </summary>
    public BuildStage[] stages = new BuildStage[0];

    /// <summary>
    /// Current (active) stage
    /// </summary>
    public BuildStage currentBuildStage;

    /// <summary>
    /// Initializes the build stage
    /// </summary>
    public void Init()
    {
        // do we have any stages connected?
        if (stages.Length == 0)
            throw new ArgumentException("BuildStageCollection must have atleast one stage");

        remaining = stages.Length;
        nextIndex = 0;

        currentBuildStage = GetNext();
        currentBuildStage.Init();
    }

    /// <summary>
    /// Checks how many stages are remaining
    /// </summary>
    /// <returns>Number of remaining stages</returns>
    public int Remaining()
    {
        return remaining;
    }

    /// <summary>
    /// Get next stage from the queue
    /// </summary>
    /// <returns>Next stage from the queue</returns>
    public BuildStage GetNext()
    {
        if (nextIndex != stages.Length)
        {
            BuildStage nextBuildStage = stages[nextIndex];
            nextIndex++;
            remaining--;
            return nextBuildStage;
        }
        else
            throw new ArgumentOutOfRangeException("BuildStageCollection: GetNext on last item");
    }
}
