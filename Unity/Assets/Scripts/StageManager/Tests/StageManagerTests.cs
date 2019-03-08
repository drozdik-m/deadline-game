using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManagerTests : MonoBehaviour
{
    StageManager stageManager;

    private void Start()
    {
        stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
    }

    public void NextStage()
    {
        stageManager.NextStage();
    }
}
