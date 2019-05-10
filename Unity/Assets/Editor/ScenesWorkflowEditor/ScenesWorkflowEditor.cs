using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CustomEditor(typeof(ScenesWorkflow))]
public class ScenesWorkflowEditor : DefaultEditor<ScenesWorkflow>
{
    ArrayEditor<ScenesWorkflow, WorkflowScene, WorkflowSceneEditor> scenesArray;

    public ScenesWorkflowEditor()
    {
        scenesArray = new ArrayEditor<ScenesWorkflow, WorkflowScene, WorkflowSceneEditor>("WorkflowScenes", MessageBox);
    }

    public override void OnCustomInspectorGUI()
    {
        Target.StoryScenes = scenesArray.Use(Target);
    }
}
