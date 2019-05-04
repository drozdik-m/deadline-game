using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DebugReaction))]
public class DebugReactionEditor : ReactionEditor
{
    public override string GetFoldoutLabel()
    {
        return "Debug Reaction";
    }

    public override void OnArrayItemInspectorGUI()
    {
        EditorGUILayout.LabelField("Debug reaction editor");
    }
}
