using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageGameObjectActivity))]
public class StageGameObjectActivityEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageGameObjectActivity";
    }

    public override void OnStageInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
