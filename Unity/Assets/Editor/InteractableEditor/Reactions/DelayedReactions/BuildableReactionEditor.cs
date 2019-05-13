using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom editor for Buildable Reaction
/// </summary>
[CustomEditor(typeof(BuildableReaction))]
public class BuildableReactionEditor : ReactionEditor
{
    public override string GetFoldoutLabel()
    {
        return "Buildable Reaction";
    }

    public override void OnCustomInspectorGUI()
    {
        BuildableReaction thisReaction = Target as BuildableReaction;

        thisReaction.delay = EditorGUILayout.Slider("Delay", thisReaction.delay, 0, 5);
        thisReaction.buildableObject = (BuildableObject)EditorGUILayout.ObjectField("Buildable Object",
                                                                                   thisReaction.buildableObject,
                                                                                   typeof(BuildableObject),
                                                                                   true);

        if (thisReaction.buildableObject == null)
            MessageBox.AddMessage("Buildable Object is empty", WarningStyle);
    }
}
