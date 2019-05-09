using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageFade))]
public class StageFadeEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageFade";
    }

    public override void OnStageInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
