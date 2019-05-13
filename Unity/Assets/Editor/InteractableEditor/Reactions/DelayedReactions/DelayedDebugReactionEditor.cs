using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom editor for Delayed Debug Reaction
/// </summary>
[CustomEditor(typeof(DelayedDebugReaction))]
public class DelayedDebugReactionEditor : ReactionEditor
{
    public override string GetFoldoutLabel()
    {
        return "Delayed Debug Reaction";
    }

    public override void OnCustomInspectorGUI()
    {
        DelayedDebugReaction thisReaction = Target as DelayedDebugReaction;
        thisReaction.debugMessage = EditorGUILayout.TextField("Message", thisReaction.debugMessage);
        thisReaction.delay = EditorGUILayout.Slider("Delay", thisReaction.delay, 0, 5);
    }
}
