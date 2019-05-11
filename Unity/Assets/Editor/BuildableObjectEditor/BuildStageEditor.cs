using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor for Build Stage
/// </summary>
public abstract class BuildStageEditor : DefaultEditor<BuildStage>, IArrayItemEditor
{
    public abstract string GetFoldoutLabel();

    public override void OnCustomInspectorGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gameObjectsToActive"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gameObjectsToHide"), true);

        OnBuildStageInspectorGUI();

        serializedObject.ApplyModifiedProperties();

        if (Target.gameObjectsToActive.Length == 0 && Target.gameObjectsToHide.Length == 0)
            MessageBox.AddMessage("No Game Objects will be affected by this stage (nothing selected)", WarningStyle);
        else
        {
            for (int i = 0; i < Target.gameObjectsToActive.Length; i++)
            {
                if (Target.gameObjectsToActive[i] == null)
                {
                    MessageBox.AddMessage("Game Object in 'Active' on index '" + i + "' is empty", WarningStyle);
                    return;
                }
            }

            for (int i = 0; i < Target.gameObjectsToHide.Length; i++)
            {
                if (Target.gameObjectsToHide[i] == null)
                {
                    MessageBox.AddMessage("Game Object in 'Hide' on index '" + i + "' is empty", WarningStyle);
                    return;
                }
            }
        }  
    }

    protected virtual void OnBuildStageInspectorGUI()
    {

    }

}
