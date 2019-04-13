using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that en/fidables GameObjects on stage start
/// </summary>
public class StageGameObjectActivity : Stage
{
    /// <summary>
    /// GameObjects to handle with
    /// </summary>
    public GameObject[] gameObjectsToHandle;

    /// <summary>
    /// Should the gameobject en/disable?
    /// </summary>
    public GameObjectActivity setting;

    /// <summary>
    /// Set "ready" flag, moves to next stage immidiately if true.
    /// </summary>
    public bool readyForNextState = false;

    public override bool ReadyForNextStage()
    {
        return readyForNextState;
    }

    public override void StageEnd()
    {
        
    }

    public override void StageLoad()
    {
        bool targetActive = setting == GameObjectActivity.Disable ? false : true;

        for (int i = 0; i < gameObjectsToHandle.Length; i++)
            gameObjectsToHandle[i].SetActive(targetActive);
    }


    public override void StageFixedUpdate()
    {
        
    }

    public override void StageUpdate()
    {
       
    }
}

public enum GameObjectActivity
{
    Enable,
    Disable
}