using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Dialog trigger.
/// </summary>
public class DialogTrigger : MonoBehaviour
{
    /// <summary>
    /// The array of dialog scriptable objects.
    /// </summary>
    public SelfTalkDialog[] dialogs;
    /// <summary>
    /// Refference to DialogManager.
    /// </summary>
    private DialogManager dm;

    private void Start()
    {
        dm = FindObjectOfType<DialogManager>();
        if (dm == null)
            Debug.Log("Missing dialog manager!");
    }

    /// <summary>
    /// Triggers the dialog.
    /// </summary>
    /// <param name="id">Identifier.</param>
    public void TriggerDialog(int id)
    {
        if (id < 0 || id >= dialogs.Length)
        {
            Debug.Log("Out of range!");
            return;
        }
       dm.StartDialog(dialogs[id]);
    }
}
