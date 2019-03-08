using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Testing class for Stage Manager
/// </summary>
public class StageManagerTests : MonoBehaviour
{
    StageManager stageManager;

    private void Start()
    {
        stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
    }

    /// <summary>
    /// Move to next stage
    /// </summary>
    public void NextStage()
    {
        stageManager.NextStage();
    }
}
