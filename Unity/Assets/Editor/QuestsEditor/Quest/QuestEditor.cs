using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Quest))]
public class QuestEditor : DefaultEditor<Quest>
{
    ArrayEditor<Quest, QuestCondition, QuestConditionEditor> conditionsEditor;

    public QuestEditor()
    {
        Target.QuestDescription = EditorGUILayout.TextField("Quest Description", Target.QuestDescription);
        conditionsEditor = new ArrayEditor<Quest, QuestCondition, QuestConditionEditor>("Quest conditions", MessageBox);
    }

    public override void OnCustomInspectorGUI()
    {
        Target.conditions = conditionsEditor.Use(Target);
    }

}
