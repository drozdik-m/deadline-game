using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Say something but dont wait for it to finish
/// </summary>
public class StageMonologAsync : StageMonologSync
{
    public override bool ReadyForNextStage()
    {
        return true;
    }
}
