using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Testing class for Stage Manager
/// </summary>
public class StageManagerTests : MonoBehaviour
{
    public StageManager stageManager;
    public StageManager subStageManager1;
    public Text finishedOutput;

    private void Start()
    {
        //stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
        stageManager.OnStageChange += OnStageChangeDebug;
    }

    private void OnStageChangeDebug(StageManager caller, StageManagerArgs args)
    {
        Debug.Log("Stage changed");
        Debug.Log(args.CurrentStage);
    }

    private void Update()
    {
        if (stageManager.IsFinished())
            finishedOutput.text = "true";
        else
            finishedOutput.text = "false";
    }

    /// <summary>
    /// Move to next stage
    /// </summary>
    public void NextStage()
    {
        stageManager.NextStage();
    }

    /// <summary>
    /// Move to next stage of substage manager 1
    /// </summary>
    public void NextStageSub1()
    {
        subStageManager1.NextStage();
    }


}
