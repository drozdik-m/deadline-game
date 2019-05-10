using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Quest))]
public class QuestEditor : DefaultEditor<Quest>, IArrayItemEditor
{
    ArrayEditor<Quest, QuestCondition, QuestConditionEditor> conditionsEditor;

    public QuestEditor()
    {
        
        conditionsEditor = new ArrayEditor<Quest, QuestCondition, QuestConditionEditor>("Quest conditions", MessageBox);
    }

    public string GetFoldoutLabel()
    {
        return "Quest";
    }

    public override void OnCustomInspectorGUI()
    {
        Target.QuestDescription = EditorGUILayout.TextField("Quest Description", Target.QuestDescription);
        if (Target.IsCompleted())
            EditorGUILayout.LabelField("[Completed]", SuccessStyle);
        Target.conditions = conditionsEditor.Use(Target);
    }
}
