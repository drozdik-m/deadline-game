using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTriggerSimulator : MonoBehaviour
{
    public DialogManager dm;
    public TwinTalkDialog std;
    public GameObject target;
    // Start is called before the first frame update

    void Update()
    {
        if (Input.GetKeyDown("escape"))
            dm.AddDialog(std,target);
    }

}
