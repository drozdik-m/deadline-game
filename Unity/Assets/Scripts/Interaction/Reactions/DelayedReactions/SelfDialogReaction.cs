using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reaction with self dialog
/// </summary>
public class SelfDialogReaction : DelayedReaction
{
    private GameObject dialogManagerObj;
    private DialogManager dialogManager;

    /// <summary>
    /// Self dialog that will be played
    /// </summary>
    public SelfTalkDialog selfTalkDialog;

    protected override void SpecificInit()
    {
        // find dialog manager in the scene
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
        dialogManager.AddDialog(selfTalkDialog);
    }
}
