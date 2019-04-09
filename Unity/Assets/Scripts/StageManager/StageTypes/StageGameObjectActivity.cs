using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that enables GameObjects on stage start and disables them on stage end.
/// </summary>
public class StageGameObjectActivity : Stage
{
    /// <summary>
    /// GameObjects to handle with
    /// </summary>
    public GameObject[] gameObjectsToHandle;

    /// <summary>
    /// Should be handled GameObjects disabled on Awake()?
    /// </summary>
    public bool disableOnAwake = true;

    /// <summary>
    /// Should be handled GameObjects disabled on stage end?
    /// </summary>
    public bool disableObjectOnStageEnd = true;

    /// <summary>
    /// Set "ready" flag, moves to next stage immidiately if true.
    /// </summary>
    public bool readyForNextState = false;
    
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public override bool ReadyForNextStage()
    {
        return readyForNextState;
    }

    public override void StageEnd()
    {
        if (!disableObjectOnStageEnd)
            return;
        for (int i = 0; i < gameObjectsToHandle.Length; i++)
            gameObjectsToHandle[i].SetActive(false);
    }

    public override void StageLoad()
    {
        for (int i = 0; i < gameObjectsToHandle.Length; i++)
            gameObjectsToHandle[i].SetActive(true);
    }


    public override void StageFixedUpdate()
    {
        
    }

    public override void StageUpdate()
    {
       
    }
}
