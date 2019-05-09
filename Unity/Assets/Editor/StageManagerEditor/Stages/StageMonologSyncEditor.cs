using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageMonologSync))]
public class StageMonologSyncEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageMonologSync";
    }

    public override void OnStageInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
