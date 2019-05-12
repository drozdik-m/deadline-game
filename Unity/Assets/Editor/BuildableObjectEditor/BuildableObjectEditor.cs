using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor for Buildable Object
/// </summary>
[CustomEditor(typeof(BuildableObject))]
public class BuildableObjectEditor : DefaultEditor<BuildableObject>
{
    private void AddInitialStageOpt()
    {
        if (Target.initialStage == null)
        {
            EditorGUILayout.LabelField("Initial stage is empty", WarningStyle);
            if (GUILayout.Button("Add Initial Stage"))
            {
                if (Target.gameObject.GetComponent<InitialStage>() == null)
                    Target.initialStage = Target.gameObject.AddComponent<InitialStage>();
                else
                    Target.initialStage = Target.gameObject.GetComponent<InitialStage>();
            }
        }
    }

    private void AddBuildStageCollectionOpt()
    {
        if (Target.stageObjectCollection == null)
        {
            EditorGUILayout.LabelField("Build Stage Collection is empty", WarningStyle);
            if (GUILayout.Button("Add Build Stage Collection"))
            {
                if (Target.gameObject.GetComponent<BuildStageCollection>() == null)
                    Target.stageObjectCollection = Target.gameObject.AddComponent<BuildStageCollection>();
                else
                    Target.stageObjectCollection = Target.gameObject.GetComponent<BuildStageCollection>();
            }
        }
    }

    public override void OnCustomInspectorGUI()
    {
        AddInitialStageOpt();
        AddBuildStageCollectionOpt();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Add Initial Stage (set up buildable object for scene)");
        EditorGUILayout.LabelField("Add Build Stage Collection (building workflow)");

        EditorGUILayout.Space();

        Target.initialStage = (InitialStage)EditorGUILayout.ObjectField("Initial Stage",
                                                                         Target.initialStage,
                                                                         typeof(InitialStage),
                                                                         true);

        Target.stageObjectCollection = (BuildStageCollection)EditorGUILayout.ObjectField("Build Stage Collection",
                                                                                          Target.stageObjectCollection,
                                                                                          typeof(BuildStageCollection),
                                                                                          true);
    }
}
