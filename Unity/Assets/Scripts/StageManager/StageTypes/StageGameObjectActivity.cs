using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that enables GameObjects on stage start and disables them on stage end.
/// </summary>
public class StageGameObjectActivity : Stage
{
    public GameObject[] gameObjectsToHandle;
    public bool disableOnAwake = true;
    public bool disableObjectOnStageEnd = true;
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
