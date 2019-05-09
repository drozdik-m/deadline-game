using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom editor for Condition Reaction
/// </summary>
[CustomEditor(typeof(ConditionReaction))]
public class ConditionReactionEditor : ReactionEditor
{
    public override string GetFoldoutLabel()
    {
        return "Condition Reaction";
    }

    public override void OnCustomInspectorGUI()
    {
        ConditionReaction thisReaction = Target as ConditionReaction;

        thisReaction.condition = (Condition)EditorGUILayout.ObjectField("Condition",
                                                                         thisReaction.condition,
                                                                         typeof(Condition),
                                                                         true);

        thisReaction.satisfied = EditorGUILayout.Toggle("Satisfied", thisReaction.satisfied);

        if (thisReaction.condition == null)
            MessageBox.AddMessage("Condition is empty", DefaultEditor<MonoBehaviour>.WarningStyle);
    }
}
