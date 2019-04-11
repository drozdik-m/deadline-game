using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom editor for reaction event
/// </summary>
[CustomEditor(typeof(ReactionEvent))]
public class ReactionEventEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Event Reaction";
    }
}
