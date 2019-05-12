using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageManual))]
public class StageManualEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageManual";
    }

    public override void OnStageInspectorGUI()
    {
        StageManual Target = base.Target as StageManual;

        //Move
        Target.MoveToNextStage = EditorGUILayout.Toggle("Move to next stage", Target.MoveToNextStage);
    }
}
