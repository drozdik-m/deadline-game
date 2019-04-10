using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDebugReaction : DelayedReaction
{
    /// <summary>
    /// Message that will be printed into console
    /// </summary>
    public string debugMessage;

    protected override void ImmediateReaction()
    {
        Debug.Log(debugMessage);
    }
}
