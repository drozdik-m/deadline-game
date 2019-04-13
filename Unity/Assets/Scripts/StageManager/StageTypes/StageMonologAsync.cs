using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMonologAsync : StageMonologSync
{
    public override bool ReadyForNextStage()
    {
        return true;
    }
}
