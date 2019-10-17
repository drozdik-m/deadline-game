using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageWait))]
public class StageWaitEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageWait";
    }

    public override void OnStageInspectorGUI()
    {
        StageWait Target = base.Target as StageWait;

        //Wait time
        Target.waitSeconds = EditorGUILayout.FloatField("Seconds to wait", Target.waitSeconds);
        if (Target.waitSeconds < 0)
            MessageBox.AddMessage("Waiting time is negative", ErrorStyle);
        if (Target.waitSeconds == 0)
            MessageBox.AddMessage("Waiting time is zero", WarningStyle);
    }
}
