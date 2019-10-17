using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManual : Stage
{

    public bool MoveToNextStage
    {
        get
        {
            return moveToNextStage;
        }
        set
        {
            moveToNextStage = value;
        }
    }

    [SerializeField]
    bool moveToNextStage = false;


    public override bool ReadyForNextStage()
    {
        return MoveToNextStage;
    }

    public override void StageEnd()
    {
        
    }

    public override void StageFixedUpdate()
    {
        
    }

    public override void StageLoad()
    {
        MoveToNextStage = false;
    }

    public override void StageUpdate()
    {
        
    }
}
