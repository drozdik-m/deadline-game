using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageChangeDaytime))]
public class StageChangeDaytimeEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageChangeDaytime";
    }

    public override void OnStageInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
