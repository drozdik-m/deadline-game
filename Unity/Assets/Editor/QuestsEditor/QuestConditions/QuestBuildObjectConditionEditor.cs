using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestBuildObjectCondition))]
public class QuestBuildObjectConditionEditor : QuestConditionEditor
{
    public override string GetFoldoutLabel()
    {
        return "QuestBuildObjectCondition";
    }

    public override void OnConditionInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
