using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageComponentActivity))]
public class StageComponentActivityEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageComponentActivity";
    }

    public override void OnStageInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
