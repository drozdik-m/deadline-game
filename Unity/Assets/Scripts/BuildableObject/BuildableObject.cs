using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObject : MonoBehaviour
{
    // přidat OnStageChange a OnStageFinished eventy (do EventArgs dát IsFinished a CurrentState, silně typovaně);
    // 
    // metoda IsFinished();

    BuildStageCollection stageObjectCollection;
    private BuildStage currentStage;

    private void Start()
    {
        stageObjectCollection = GetComponent<BuildStageCollection>();
        stageObjectCollection.Init();

        currentStage = stageObjectCollection.currentBuildStage;
    }

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
    /// Builds next stage (conditions are supposed to be satisfied)
    /// </summary>
    private void buildNextStage()
    {
        currentStage = stageObjectCollection.GetNext();
        currentStage.Init();
    }


}
