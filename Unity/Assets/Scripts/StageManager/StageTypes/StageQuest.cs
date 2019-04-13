using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageQuest : Stage
{
    public Quest quest;

    public override bool ReadyForNextStage()
    {
        return quest.IsCompleted();
    }

    public override void StageEnd()
    {
        
    }

    public override void StageFixedUpdate()
    {
        
    }

    public override void StageLoad()
    {
        quest.Recording = true;
    }

    public override void StageUpdate()
    {
        
    }
}
