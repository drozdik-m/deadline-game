using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor for No Item Stage Editor
/// </summary>
[CustomEditor(typeof(NoItemStage))]
public class NoItemStageEditor : BuildStageEditor, IArrayItemEditor
{
    public override string GetFoldoutLabel()
    {
        return "No Item Stage";
    }

    protected override void OnBuildStageInspectorGUI()
    {

    }
}
