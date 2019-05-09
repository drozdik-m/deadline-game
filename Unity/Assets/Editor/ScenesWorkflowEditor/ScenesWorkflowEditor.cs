using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CustomEditor(typeof(ScenesWorkflow))]
public class ScenesWorkflowEditor : DefaultEditor<ScenesWorkflow>
{
    ArrayEditor<ScenesWorkflow, WorkflowScene, WorkflowSceneEditor> arrEditor;

    public ScenesWorkflowEditor()
    {
        arrEditor = new ArrayEditor<ScenesWorkflow, WorkflowScene, WorkflowSceneEditor>("ScenesWorkflow", MessageBox);
    }

    public override void OnCustomInspectorGUI()
    {
        Target.StoryScenes = arrEditor.Use(Target);
    }
}
