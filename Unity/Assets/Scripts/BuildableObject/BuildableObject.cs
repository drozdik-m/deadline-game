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
    private bool isFinished;

    /// <summary>
    /// Collections of stages
    /// </summary>
    public BuildStageCollection stageObjectCollection;

    /// <summary>
    /// initial stage for setting up visibility of gameobjects
    /// </summary>
    public InitialStage initialStage;

    /// <summary>
    /// Next stage to come
    /// </summary>
    private BuildStage nextStage;

    private void Start()
    {
        isFinished = false;
        initialStage.Load();
        initialStage.Init();
        stageObjectCollection = GetComponent<BuildStageCollection>();

        nextStage = stageObjectCollection.GetNext();

        if (nextStage == null)
            Debug.LogError("Buildable object has no other stage than initial. Add atleast one stage to buildable object");
    }

    /// <summary>
    /// Attempts going to next stage, if conditions are satisfied, next stage will be intialized
    /// </summary>
    /// <returns>True if conditions are satisfied and next stage is build, false if not</returns>
    public bool AttemptNextStage()
    {
        if (nextStage == null || !nextStage.ConditionsSatisfied())
            return false;  
        else
        {
            nextStage.Init();
            BuildStage currStage = nextStage;
            nextStage = stageObjectCollection.GetNext();
            nextStage.Load();

            // we are finished
            if (nextStage == null)
                isFinished = true;

            OnChange?.Invoke(this, new BuildStageChangeEventArgs(currStage, IsFinished()));

            if (IsFinished())
                OnFinished?.Invoke(this, new BuildStageFinishedEventArgs(currStage));

            return true;
        }
    }

    /// <summary>
    /// Checks if the buildable object is in its final stage
    /// </summary>
    /// <returns>True if buildable object is in its final stage, false if not</returns>
    public bool IsFinished()
    {
        return isFinished;
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
