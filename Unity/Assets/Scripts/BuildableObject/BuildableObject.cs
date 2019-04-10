using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObject : MonoBehaviour
{
    public BuildStageCollection stageObjectCollection
        = new BuildStageCollection();
    private BuildStage currentStage;

    private void Start()
    {
        if (stageObjectCollection.Count() < 1)
            Debug.Log("Buildable object '" + name + "' must have atleast one stage");
        else
            currentStage = stageObjectCollection.Dequeue();
        
        stageObjectCollection.Init();
    }

    public bool AttemptNextStage()
    {
        if (stageObjectCollection.Count() == 0 || !currentStage.ConditionsSatisfied())
        {
            // here show dialog
            Debug.Log("Conditions for building next stage are not satisfied");
            return false;
        }
            
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
        currentStage.Dismiss();
        currentStage = stageObjectCollection.Dequeue();
        currentStage.Init();
    }
}
