using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Buildable object represents an object that contains several stages, that player
/// can go through in the game (Stage1 -> Stage2 -> Stage3 -> ...)
/// </summary>
public class BuildableObject : MonoBehaviour
{
    /// <summary>
    /// Collections of stages
    /// </summary>
    BuildStageCollection stageObjectCollection;

    /// <summary>
    /// Current (active) stage
    /// </summary>
    private BuildStage currentStage;

    private void Start()
    {
        stageObjectCollection = GetComponent<BuildStageCollection>();
        stageObjectCollection.Init();

        currentStage = stageObjectCollection.currentBuildStage;
    }

    /// <summary>
    /// Attempts going to next stage, if conditions are satisfied, next stage will be intialized
    /// </summary>
    /// <returns>True if conditions are satisfied and next stage is build, false if not</returns>
    public bool AttemptNextStage()
    {
        if (stageObjectCollection.Remaining() == 0 || !currentStage.ConditionsSatisfied())
            return false;  
        else
        {
            buildNextStage();
            return true;
        }
    }

    /// <summary>
    /// Checks if the buildable object is in its final stage
    /// </summary>
    /// <returns>True if buildable object is in its final stage, false if not</returns>
    public bool IsFinished()
    {
        return stageObjectCollection.Remaining() == 0;
    }

    /// <summary>
    /// Builds next stage (conditions are supposed to be satisfied)
    /// </summary>
    private void buildNextStage()
    {
        currentStage = stageObjectCollection.GetNext();
        currentStage.Init();

        // call events
        OnChange?.Invoke(this, new BuildStageChangeEventArgs(currentStage, IsFinished()));

        if (IsFinished())
            OnFinished?.Invoke(this, new BuildStageFinishedEventArgs(currentStage));
    }

    /// <summary>
    /// Event handler for OnChange event
    /// </summary>
    /// <param name="caller">Buildable object on which stage changed</param>
    /// <param name="e">event arguments with information of change</param>
    public delegate void BuildStageChangeEventHandler(BuildableObject caller, BuildStageChangeEventArgs e);

    /// <summary>
    /// Event called on build stage change
    /// </summary>
    public event BuildStageChangeEventHandler OnChange;

    /// <summary>
    /// Event handler for OnFinished event
    /// </summary>
    /// <param name="caller">Buildable object on which stage finished</param>
    /// <param name="e">event arguments with information of finish</param>
    public delegate void BuildStageFinishedEventHandler(BuildableObject caller, BuildStageFinishedEventArgs e);

    /// <summary>
    /// Event called on build stage finished
    /// </summary>
    public event BuildStageFinishedEventHandler OnFinished;
}

public class BuildStageChangeEventArgs
{
    public BuildStage buildStage;
    public bool isFinished;

    public BuildStageChangeEventArgs(BuildStage bs, bool isFin)
    {
        buildStage = bs;
        isFinished = isFin;
    }
}

public class BuildStageFinishedEventArgs
{
    public BuildStage buildStage;

    public BuildStageFinishedEventArgs(BuildStage bs)
    {
        buildStage = bs;
    }
}