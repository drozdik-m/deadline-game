﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reaction that handles dialogs
/// </summary>
public class DialogReaction : Reaction
{
    // sem si lze pridat jakekoliv public atributy potrebne k zavolani dialogu
    private GameObject dialogManagerObj;
    private DialogManager dialogManager;

    public TwinTalkDialog twinTalkDialog;
    public SelfTalkDialog selfTalkDialog;

    public GameObject target;

    protected override void SpecificInit()
    {
        dialogManagerObj = GameObject.FindGameObjectWithTag("DialogManager");
        if (dialogManagerObj == null)
        {
            Debug.LogError("Dialog Manager was not found -> you need to add it to the scene");
            return;
        }

        dialogManager = dialogManagerObj.GetComponent<DialogManager>();
        if (dialogManager == null)
        {
            Debug.LogError("Dialog Manager Object does not have Dialog Manager Compoment -> you need to add it");
            return;
        }
    }

    protected override void ImmediateReaction()
    {
        if (twinTalkDialog != null)
            dialogManager.AddDialog(twinTalkDialog, target);
        else
            dialogManager.AddDialog(selfTalkDialog);
    }

}
