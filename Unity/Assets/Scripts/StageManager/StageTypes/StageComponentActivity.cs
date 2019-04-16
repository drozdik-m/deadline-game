using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageComponentActivity : Stage
{
    /// <summary>
    /// Should the component en/disable?
    /// </summary>
    public ObjectActivity setting;

    /// <summary>
    /// Array of components to handle
    /// </summary>
    public Behaviour[] componentsToHandle;

    /// <summary>
    /// Set "ready" flag, moves to next stage immidiately if true.
    /// </summary>
    public bool readyForNextState = true;

    public override bool ReadyForNextStage()
    {
        return readyForNextState;
    }

    public override void StageEnd()
    {
        
    }

    public override void StageFixedUpdate()
    {
       ;
    }

    public override void StageLoad()
    {
        bool targetActive = setting == ObjectActivity.Disable ? false : true;

        for (int i = 0; i < componentsToHandle.Length; i++)
            componentsToHandle[i].enabled = targetActive;
    }

    public override void StageUpdate()
    {
        
    }
}
