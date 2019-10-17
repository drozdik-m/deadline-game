using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stage manager class that handles stages
/// </summary>
public class StageManager : MonoBehaviour
{
    /// <summary>
    /// Stages to handle. Moves from first to last.
    /// </summary>
    public Stage[] stages;

    /// <summary>
    /// Current stage index
    /// </summary>
    int currentStage = 0;

    /// <summary>
    /// Event invoked when stage is changed
    /// </summary>
    public event StageHandler OnStageChange;

    /// <summary>
    /// Should the stage manager start on load?
    /// </summary>
    public bool AutomaticStart = false;

    /// <summary>
    /// Is the stage manager initiated and running?
    /// </summary>
    bool initiated = false;

    private void Start()
    {
        if (AutomaticStart)
            InitiateStages();
    }

    private void Update()
    {
        //Is there any stage?
        if (!IsAnyStageActive())
            return;

        //Swap stages
        if (stages[currentStage].ReadyForNextStage())
            NextStage();

        //Stage update
        if (IsAnyStageActive())
            stages[currentStage].StageUpdate();
    }

    private void FixedUpdate()
    {
        //Is there any stage?
        if (!IsAnyStageActive())
            return;

        //Swap stages
        if (stages[currentStage].ReadyForNextStage())
            NextStage();

        //Stage update
        if (IsAnyStageActive())
            stages[currentStage].StageFixedUpdate();
    }

    /// <summary>
    /// Initiate the Stage manager. Should be called only once.
    /// </summary>
    public void InitiateStages()
    {
        initiated = true;
        LoadFirstStage();
    }

    /// <summary>
    /// Load first stage (for init only)
    /// </summary>
    private void LoadFirstStage()
    {
        //Is there any stage?
        if (!IsAnyStageActive())
            return;

        stages[0].StageLoad();

        //Trigger OnStageChange event
        OnStageChange?.Invoke(this, new StageManagerArgs(stages[currentStage]));
    }

    /// <summary>
    /// Returns current stage object.
    /// </summary>
    /// <returns>Returns current stage</returns>
    public Stage GetCurrentStage()
    {
        if (!IsAnyStageActive())
            return null;

        return stages[currentStage];
    }

    /// <summary>
    /// Move to next stage immidiately
    /// </summary>
    public void NextStage()
    {
        //Is there loaded stage?
        if (!IsAnyStageActive())
            return;

        //Trigger end
        stages[currentStage++].StageEnd();
        
        //Are we at end?
        if (!IsAnyStageActive())
            return;

        //Load new stage
        stages[currentStage].StageLoad();

        //Trigger OnStageChange event
        OnStageChange?.Invoke(this, new StageManagerArgs(stages[currentStage]));
    }

    /// <summary>
    /// Is there and stage left?
    /// </summary>
    /// <returns>True if some stage is active</returns>
    public bool IsAnyStageActive()
    {
        return currentStage < stages.Length && initiated;
    }

    /// <summary>
    /// Tells if the stage manager is finished or not
    /// </summary>
    /// <returns>True if stage manager is finished, else false</returns>
    public bool IsFinished()
    {
        return !IsAnyStageActive() && initiated;
    }
}


public delegate void StageHandler(StageManager caller, StageManagerArgs args);