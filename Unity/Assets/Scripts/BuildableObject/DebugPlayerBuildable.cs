using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayerBuildable : MonoBehaviour
{
    public BuildableObject buildableObject;
    // Start is called before the first frame update

    public void NextStage()
    {
        if (buildableObject.AttemptNextStage())
            Debug.Log("Next stage approved");
        else
            Debug.Log("Next stage cannot be done");
    }
}
