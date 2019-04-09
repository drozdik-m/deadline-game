using UnityEngine;

/// <summary>
/// Debug reaction is debugging tool for testing reactions
/// </summary>
public class DebugReaction : Reaction
{
    /// <summary>
    /// message that will be printed into console
    /// </summary>
    public string debugMessage;

    protected override void ImmediateReaction()
    {
        Debug.Log(debugMessage);
    }
}