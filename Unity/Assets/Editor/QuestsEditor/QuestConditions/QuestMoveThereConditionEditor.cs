using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestMoveThereCondition))]
public class QuestMoveThereConditionEditor : QuestConditionEditor
{
    public override string GetFoldoutLabel()
    {
        return "QuestMoveThereCondition";
    }

    public override void OnConditionInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
