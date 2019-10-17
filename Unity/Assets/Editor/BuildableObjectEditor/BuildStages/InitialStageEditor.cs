using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor for Initial Stage
/// </summary>
[CustomEditor(typeof(InitialStage))]
public class InitialStageEditor : BuildStageEditor
{
    public override string GetFoldoutLabel()
    {
        return "Initial Stage Editor";
    }
}
