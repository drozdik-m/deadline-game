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
        DrawDefaultInspector();
    }
}
