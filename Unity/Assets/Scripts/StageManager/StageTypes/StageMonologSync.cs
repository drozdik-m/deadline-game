using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Say monolog. Waits for it to end
/// </summary>
public class StageMonologSync : Stage
{
    /// <summary>
    /// Override dialog manager
    /// </summary>
    public DialogManager OverrideDialogManager;

    /// <summary>
    /// Actual working manager
    /// </summary>
    DialogManager dialogManager;

    /// <summary>
    /// What should it say?
    /// </summary>
    public SelfTalkDialog WhatToSay;

    public override bool ReadyForNextStage()
    {
        return !dialogManager.DialogInProgress();
    }

    public override void StageEnd()
    {
        
    }

    public override void StageFixedUpdate()
    {
       
    }

    public override void StageLoad()
    {
        dialogManager = OverrideDialogManager != null ? OverrideDialogManager :
            GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();

        dialogManager.AddDialog(WhatToSay);
    }

    public override void StageUpdate()
    {
       
    }
}
