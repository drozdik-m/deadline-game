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
    public GameObject[] GameObjectsToHandle = new GameObject[0];

    /// <summary>
    /// Should the gameobject en/disable?
    /// </summary>
    public ObjectActivity Setting;


    public override bool ReadyForNextStage()
    {
        return true;
    }

    public override void StageEnd()
    {
        
    }

    public override void StageLoad()
    {
        bool targetActive = Setting == ObjectActivity.Disable ? false : true;

        for (int i = 0; i < GameObjectsToHandle.Length; i++)
            GameObjectsToHandle[i].SetActive(targetActive);
    }


    public override void StageFixedUpdate()
    {
        
    }

    public override void StageUpdate()
    {
       
    }
}

public enum ObjectActivity
{
    Enable,
    Disable
}