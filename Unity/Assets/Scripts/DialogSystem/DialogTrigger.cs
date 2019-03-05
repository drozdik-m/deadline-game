using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public SelfTalkDialog[] dialogs;
    private DialogManager dm;

    private void Start()
    {
        dm = FindObjectOfType<DialogManager>();
        if (dm == null)
            Debug.Log("Missing dialog manager!");
    }

    public void TriggerDialog(int id)
    {
        if (id < 0 || id >= dialogs.Length)
        {
            Debug.Log("Out of range!");
            return;
        }

        dm.startDialog(dialogs[id]);
    }


}
