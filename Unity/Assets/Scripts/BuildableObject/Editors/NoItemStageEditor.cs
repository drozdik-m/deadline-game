using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NoItemStage))]
public class NoItemStageEditor : BuildStageEditor
{
    protected override string GetFoldoutLabel()
    {
        return "No Item Stage";
    }
}
