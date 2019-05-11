using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBuildObjectCondition : QuestCondition
{
    public BuildableObject BuildableObject = null;

    private void Start()
    {
        BuildableObject.OnFinished += BuildableObjectFinished;
        Completed = false;
    }

    private void BuildableObjectFinished(BuildableObject caller, BuildStageFinishedEventArgs e)
    {
        Completed = true;
    }

}
