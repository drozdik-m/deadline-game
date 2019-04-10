using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConsumeItemStage))]
public class ConsumeItemStageEditor : BuildStageEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Consume Item Stage";
    }
}
