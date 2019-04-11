using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reaction for making dialog with the target
/// </summary>
public class TwinDialogReaction : Reaction
{
    private GameObject dialogManagerObj;
    private DialogManager dialogManager;

    /// <summary>
    /// Twin dialog to be played
    /// </summary>
    public TwinTalkDialog twinTalkDialog;

    /// <summary>
    /// Target with who we are going to make dialog with
    /// </summary>
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
        dialogManager.AddDialog(twinTalkDialog, target);
    }
}
