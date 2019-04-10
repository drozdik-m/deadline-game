using System;
using UnityEditor;
using UnityEngine;

public abstract class BuildStageEditor : Editor
{
    public bool showBuildStage;
    public SerializedProperty buildStagesProperty;
    private BuildStage buildStage;

    private const float buttonWidth = 30f;

    private void OnEnable()
    {
        buildStage = (BuildStage)target;
        Init();
    }

    private void Init() { }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();

        showBuildStage = EditorGUILayout.Foldout(showBuildStage, GetFoldoutLabel());

        if (GUILayout.Button("-", GUILayout.Width(buttonWidth)))
            buildStagesProperty.RemoveFromObjectArray(buildStage);

        EditorGUILayout.EndHorizontal();

        if (showBuildStage)
            DrawBuildStage();

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }

    public static BuildStage CreateBuildStage(Type buildStageType)
    {
        return (BuildStage)CreateInstance(buildStageType);
    }


    private void DrawBuildStage()
    {
        DrawDefaultInspector();
    }

    protected abstract string GetFoldoutLabel();
}
