using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom editor for delayed debug reaction
/// </summary>
[CustomEditor(typeof(DelayedDebugReaction))]
public class DelayedDebugReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Delayed Debug Reaction";
    }
}

