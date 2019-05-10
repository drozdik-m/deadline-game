using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageMonologAsync))]
public class StageMonologAsyncEditor : StageMonologSyncEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageMonologAsync";
    }

    /*public override void OnStageInspectorGUI()
    {
        DrawDefaultInspector();
    }*/
}
