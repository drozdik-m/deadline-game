using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor for Debug Reaction Editor
/// </summary>
[CustomEditor(typeof(DebugReaction))]
public class DebugReactionEditor : ReactionEditor
{
    public override string GetFoldoutLabel()
    {
        return "Debug Reaction";
    }

    public override void OnCustomInspectorGUI()
    {
        DebugReaction thisReaction = Target as DebugReaction;
        thisReaction.debugMessage = EditorGUILayout.TextField("Message", thisReaction.debugMessage);
    }

    
}
