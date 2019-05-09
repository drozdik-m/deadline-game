using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageComponentActivity : Stage
{
    /// <summary>
    /// Should the component en/disable?
    /// </summary>
    public ObjectActivity Setting = ObjectActivity.Enable;

    /// <summary>
    /// Array of components to handle
    /// </summary>
    public Behaviour[] ComponentsToHandle = new Behaviour[0];


    public override bool ReadyForNextStage()
    {
        return true;
    }

    public override void StageEnd()
    {
        
    }

    public override void StageFixedUpdate()
    {
       
    }

    public override void StageLoad()
    {
        bool targetActive = Setting == ObjectActivity.Disable ? false : true;

        for (int i = 0; i < ComponentsToHandle.Length; i++)
            ComponentsToHandle[i].enabled = targetActive;
    }

    public override void StageUpdate()
    {
        
    }
}
