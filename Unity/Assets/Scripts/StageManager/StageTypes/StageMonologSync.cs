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
    public DialogManager DialogManager = null;

    /// <summary>
    /// What should it say?
    /// </summary>
    public SelfTalkDialog WhatToSay;

    public override bool ReadyForNextStage()
    {
        if (DialogManager == null)
            return false;
        return !DialogManager.DialogInProgress();
    }

    public override void StageEnd()
    {
        
    }

    public override void StageFixedUpdate()
    {
       
    }

    public override void StageLoad()
    {
        if (DialogManager == null)
            DialogManager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();

        DialogManager.AddDialog(WhatToSay);
    }

    public override void StageUpdate()
    {
       
    }
}
