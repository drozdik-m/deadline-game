using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageManager))]
public class StageManagerEditor : DefaultEditor<StageManager>
{
    ArrayEditor<StageManager, Stage, StageEditor> stagesEditor;

    public StageManagerEditor()
    {
        stagesEditor = new ArrayEditor<StageManager, Stage, StageEditor>("Stages", MessageBox);
    }

    public override void OnCustomInspectorGUI()
    {
        Target.stages = stagesEditor.Use(Target);
    }

}
