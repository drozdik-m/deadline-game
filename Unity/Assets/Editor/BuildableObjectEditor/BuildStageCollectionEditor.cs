using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor for Build Stage Collection
/// </summary>
[CustomEditor(typeof(BuildStageCollection))]
public class BuildStageCollectionEditor : DefaultEditor<BuildStageCollection>
{
    ArrayEditor<BuildStageCollection, BuildStage, BuildStageEditor> arrEditor;

    public BuildStageCollectionEditor()
    {
        arrEditor =
            new ArrayEditor<BuildStageCollection, BuildStage, BuildStageEditor>("buildStageCollection",
                                                                                MessageBox);
    }

    public override void OnCustomInspectorGUI()
    {
        EditorGUILayout.LabelField("Build Stage Collection");
        Target.stages = arrEditor.Use(Target);
    }
}
