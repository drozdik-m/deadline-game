using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog : MonoBehaviour
{
    public string characterName;

    [TextArea(2,10)]
    public string[] sentences;


    public void triggerDialog()
    {
       DialogManager dm = FindObjectOfType<DialogManager>();
       if (dm == null)
        {
            Debug.Log("Missing Dialog manager!");
            return;
        }

        dm.startDialog(this);
    }
}
