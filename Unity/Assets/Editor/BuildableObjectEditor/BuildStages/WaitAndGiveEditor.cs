using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor for No Item Stage Editor
/// </summary>
[CustomEditor(typeof(WaitAndGive))]
public class WaitAndGiveEditor : BuildStageEditor, IArrayItemEditor
{
    public override string GetFoldoutLabel()
    {
        return "WaitAndGive";
    }

    protected override void OnBuildStageInspectorGUI()
    {
        //DrawDefaultInspector();
    }
}
