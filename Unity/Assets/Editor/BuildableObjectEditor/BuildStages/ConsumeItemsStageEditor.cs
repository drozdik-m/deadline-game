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
        //DrawDefaultInspector();
        var arrayProperty = new SerializedObject(target).FindProperty("RequiredItems");
        var delayProperty = new SerializedObject(target).FindProperty("Delay");
        new SerializedObject(target).Update();
        EditorGUILayout.PropertyField(arrayProperty, true);
        EditorGUILayout.PropertyField(delayProperty, true);
        new SerializedObject(target).ApplyModifiedProperties();

    }
}
