using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageReactionCondition))]
public class StageReactionConditionEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageReactionCondition";
    }

    public override void OnStageInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
