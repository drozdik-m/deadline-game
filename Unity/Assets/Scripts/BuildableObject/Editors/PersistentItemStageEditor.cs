using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PersistentItemStage))]
public class PersistentItemStageEditor : BuildStageEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Persistent Item Stage";
    }
}
