using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageQuestStack : Stage
{
    public QuestStack QuestStack = null;

    public override bool ReadyForNextStage()
    {
        if (QuestStack == null)
            return false;
        return QuestStack.QuestsAreCompleted();
    }

    public override void StageEnd()
    {
        
    }

    public override void StageFixedUpdate()
    {
        
    }

    public override void StageLoad()
    {
        if (QuestStack == null)
            QuestStack = GetComponent<QuestStack>();
        QuestStack.Recording = true;
    }

    public override void StageUpdate()
    {
        
    }
}
