using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reaction that handles dialogs
/// </summary>
public class DialogReaction : Reaction
{
    // sem si lze pridat jakekoliv public atributy potrebne k zavolani dialogu

    protected override void ImmediateReaction()
    {
        // tento kod probehne pri interakci
        Debug.Log("dialog reaction -> not implemented");
    }
}
