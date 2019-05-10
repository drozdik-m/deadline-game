using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWait : Stage
{
    public float waitSeconds = 1;

    public bool ReadForNextStage
    {
        get
        {
            return readyForNextStage;
        }
    }
    [SerializeField]
    bool readyForNextStage = false;


    public override bool ReadyForNextStage()
    {
        return readyForNextStage;
    }

    public override void StageEnd()
    {
        
    }

    public override void StageLoad()
    {
        readyForNextStage = false;
        StartCoroutine(Wait());
    }

    /// <summary>
    /// Waits for x seconds and sets "ready" flag to true.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitSeconds);
        readyForNextStage = true;
    }

    public override void StageFixedUpdate()
    {
        
    }

    public override void StageUpdate()
    {
        
    }
}
