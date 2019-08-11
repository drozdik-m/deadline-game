using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor for No Item Stage Editor
/// </summary>
[CustomEditor(typeof(ConsumeItemsStage))]
public class ConsumeItemsStageEditor : BuildStageEditor, IArrayItemEditor
{
    public override string GetFoldoutLabel()
    {
        return "ConsumeItemsStage";
    }

    protected override void OnBuildStageInspectorGUI()
    {
        //DrawDefaultInspector();
    }
}
