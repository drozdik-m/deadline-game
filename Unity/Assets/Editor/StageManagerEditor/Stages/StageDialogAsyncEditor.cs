using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageDialogAsync))]
public class StageDialogAsyncEditor : StageDialogSyncEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageDialogAsync";
    }

    /*public override void OnStageInspectorGUI()
    {
        DrawDefaultInspector();
    }*/
}
