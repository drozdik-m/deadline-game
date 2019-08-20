using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor for No Item Stage Editor
/// </summary>
[CustomEditor(typeof(ConsumeItemsStage))]
public class ConsumeItemsStageEditor : BuildStageEditor, IArrayItemEditor
{
    public override string GetFoldoutLabel()
    {
        return "ConsumeItemsStage";
    }

    protected override void OnBuildStageInspectorGUI()
    {
        /*
        //DrawDefaultInspector();
        var arrayProperty = new SerializedObject(Target).FindProperty("RequiredItems");
        new SerializedObject(Target).Update();
        EditorGUILayout.PropertyField(arrayProperty, true);
        new SerializedObject(Target).ApplyModifiedProperties();
        */
        EditorGUILayout.LabelField("Custom editor:");
        var serialObject = new SerializedObject(target);
        var property = serialObject.FindProperty("RequiredItems");
        serialObject.Update();
        EditorGUILayout.PropertyField(property, true);
        serialObject.ApplyModifiedProperties();

    }
}
