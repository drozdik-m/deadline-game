using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageQuestStack : Stage
{
    public QuestStack questStack;

    public override bool ReadyForNextStage()
    {
        return questStack.QuestsAreCompleted();
    }

    public override void StageEnd()
    {
        
    }

    public override void StageFixedUpdate()
    {
        
    }

    public override void StageLoad()
    {
        questStack.Recording = true;
    }

    public override void StageUpdate()
    {
        
    }
}
