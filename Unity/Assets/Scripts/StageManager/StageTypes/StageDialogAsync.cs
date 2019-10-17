using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Say something but dont wait for it to finish
/// </summary>
public class StageDialogAsync : StageDialogSync
{
    public override bool ReadyForNextStage()
    {
        return true;
    }
}
