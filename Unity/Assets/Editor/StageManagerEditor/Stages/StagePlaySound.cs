using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StagePlaySound))]
public class StagePlaySoundEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StagePlaySound";
    }

    public override void OnStageInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
