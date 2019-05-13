using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageQuest))]
public class StageQuestEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageQuest";
    }

    public override void OnStageInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
