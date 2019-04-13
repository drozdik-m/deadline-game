using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMonologSync : Stage
{
    public DialogManager OverrideDialogManager;
    DialogManager dialogManager;

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
