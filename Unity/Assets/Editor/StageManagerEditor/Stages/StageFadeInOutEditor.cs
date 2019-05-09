using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageFadeInOut))]
public class StageFadeInOutEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageFadeInOut";
    }

    public override void OnStageInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
