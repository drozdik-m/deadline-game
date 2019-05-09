using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageInteracted))]
public class StageInteractedEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageInteracted";
    }

    public override void OnStageInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
