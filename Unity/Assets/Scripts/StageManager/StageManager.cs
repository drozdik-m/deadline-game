using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Stage[] stages;

    int currentStage = 0;

    private void Start()
    {
        LoadFirstStage();
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

    private void LoadFirstStage()
    {
        //Is there any stage?
        if (!IsAnyStageActive())
            return;

        stages[0].StageLoad();
    }

    public Stage GetCurrentStage()
    {
        if (!IsAnyStageActive())
            return null;
        return stages[currentStage];
    }

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
    }

    public bool IsAnyStageActive()
    {
        return currentStage < stages.Length;
    }
}
