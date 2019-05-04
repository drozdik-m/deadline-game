using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DebugReaction))]
public class DebugReactionEditor : ReactionEditor
{
    public override void OnArrayItemInspectorGUI()
    {
        EditorGUILayout.LabelField("Debug reaction editor");
        Debug.Log("On Array Item Inspector GUI: DebugReactionEditor");
    }
}
