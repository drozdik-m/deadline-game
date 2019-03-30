using UnityEngine;

public class DebugReaction : Reaction
{
    public string debugMessage;

    protected override void SpecificInit()
    {
        base.SpecificInit();
    }

    protected override void ImmediateReaction()
    {
        Debug.Log(debugMessage);
    }
}