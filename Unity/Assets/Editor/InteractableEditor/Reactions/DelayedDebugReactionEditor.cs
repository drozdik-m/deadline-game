using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DelayedDebugReaction))]
public class DelayedDebugReactionEditor : ReactionEditor
{
    public override string GetFoldoutLabel()
    {
        return "Delayed Debug Reaction";
    }

    public override void OnArrayItemInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
