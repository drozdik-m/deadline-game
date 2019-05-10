using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestInteractCondition))]
public class QuestInteractConditionEditor : QuestConditionEditor
{
    public override string GetFoldoutLabel()
    {
        return "QuestInteractCondition";
    }

    public override void OnConditionInspectorGUI()
    {
        QuestInteractCondition Target = base.Target as QuestInteractCondition;

        Target.InteractionEventMiddleman = EditorGUILayout.ObjectField("Inveration Event Middleman", Target.InteractionEventMiddleman, typeof(InteractionEventMiddleman), true) as InteractionEventMiddleman;
        if (Target.InteractionEventMiddleman == null)
            MessageBox.AddMessage("Middleman is null", ErrorStyle);
    }
}
