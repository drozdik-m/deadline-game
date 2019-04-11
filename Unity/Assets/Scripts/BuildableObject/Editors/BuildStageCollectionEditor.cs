using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildStageCollection))]
public class BuildStageCollectionEditor : EditorWithSubEditors<BuildStageEditor, BuildStage>
{
    private BuildStageCollection buildStageCollection;
    private SerializedProperty buildStagesProperty;

    private Type[] buildStageTypes;
    private string[] buildStageTypeNames;
    private int selectedIndex;

    private const float dropAreaHeight = 50f;
    private const float controlSpacing = 5f;
    private const string buildStagesPropName = "stages";

    private readonly float verticalSpacing = EditorGUIUtility.standardVerticalSpacing;

    private void OnEnable()
    {
        buildStageCollection = (BuildStageCollection)target;

        buildStagesProperty = serializedObject.FindProperty(buildStagesPropName);

        CheckAndCreateSubEditors(buildStageCollection.stages);

        SetBuildStagesNamesArray();
    }

    private void OnDisable()
    {
        CleanupEditors();
    }

    protected override void SubEditorSetup(BuildStageEditor editor)
    {
        editor.buildStagesProperty = buildStagesProperty;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        CheckAndCreateSubEditors(buildStageCollection.stages);

        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i].OnInspectorGUI();
        }

        if (buildStageCollection.stages.Length > 0)
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        Rect fullWidthRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(dropAreaHeight + verticalSpacing));

        Rect leftAreaRect = fullWidthRect;
        leftAreaRect.y += verticalSpacing * 0.5f;
        leftAreaRect.width *= 0.5f;
        leftAreaRect.width -= controlSpacing * 0.5f;
        leftAreaRect.height = dropAreaHeight;

        Rect rightAreaRect = leftAreaRect;
        rightAreaRect.x += rightAreaRect.width + controlSpacing;

        TypeSelectionGUI(leftAreaRect);

        serializedObject.ApplyModifiedProperties();
    }

    private void TypeSelectionGUI(Rect containingRect)
    {
        Rect topHalf = containingRect;
        topHalf.height *= 0.5f;

        Rect bottomHalf = topHalf;
        bottomHalf.y += bottomHalf.height;

        selectedIndex = EditorGUI.Popup(topHalf, selectedIndex, buildStageTypeNames);

        if (GUI.Button(bottomHalf, "Add Selected Build Stage"))
        {
            Type buildStageType = buildStageTypes[selectedIndex];
            BuildStage newBuildStage = BuildStageEditor.CreateBuildStage(buildStageType);
            buildStagesProperty.AddToObjectArray(newBuildStage);
        }
    }

    private void SetBuildStagesNamesArray()
    {
        Type buildStageType = typeof(BuildStage);

        Type[] allTypes = buildStageType.Assembly.GetTypes();

        List<Type> buildStageSubTypeList = new List<Type>();

        for (int i = 0; i < allTypes.Length; i++)
            if (allTypes[i].IsSubclassOf(buildStageType) && !allTypes[i].IsAbstract)
                buildStageSubTypeList.Add(allTypes[i]);

        buildStageTypes = buildStageSubTypeList.ToArray();

        List<string> buildStageTypeNameList = new List<string>();

        for (int i = 0; i < buildStageTypes.Length; i++)
        {
            buildStageTypeNameList.Add(buildStageTypes[i].Name);
        }

        buildStageTypeNames = buildStageTypeNameList.ToArray();
    }


}
