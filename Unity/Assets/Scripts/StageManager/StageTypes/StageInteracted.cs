using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stage that awaits interaction
/// </summary>
public class StageInteracted : Stage
{
    private bool hasBeenInteractedWith = false;



    public override bool ReadyForNextStage()
    {
        return hasBeenInteractedWith;
    }

    /// <summary>
    /// Sets that an item has been interacted with
    /// </summary>
    public void HasBeenInteractedWith()
    {
        hasBeenInteractedWith = true;
    }

    public override void StageEnd()
    {
        
    }

    public override void StageFixedUpdate()
    {
        
    }

    public override void StageLoad()
    {
        
    }

    public override void StageUpdate()
    {
       
    }


}
