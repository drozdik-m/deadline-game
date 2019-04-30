using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using System;

public abstract class DefaultEditor<T> : Editor 
    where T : UnityEngine.Object
{
    bool showDefaultEditor;
    AnimBool showError = new AnimBool(false);
    Exception caughtError = null;

    public T Target
    {
        get
        {
            return target as T;
        }
    }

    public DefaultEditor(): base()
    {
        showError.valueChanged.AddListener(Repaint);
    }

    public override void OnInspectorGUI()
    {
        //Default editor toggle
        showDefaultEditor = GUILayout.Toggle(showDefaultEditor, "Show the default editor");
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        //Show caught error
        showError.target = caughtError != null;
        if (EditorGUILayout.BeginFadeGroup(showError.faded))
        {
            EditorGUILayout.BeginHorizontal();

            string errorMessage = caughtError == null ? "Error cleared" : "Caught error: " + caughtError.Message;
            EditorGUILayout.LabelField(errorMessage);

            if (GUILayout.Button("Clear"))
                caughtError = null;

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }
        EditorGUILayout.EndFadeGroup();

        
        //Show the default editor
        if (showDefaultEditor)
            base.OnInspectorGUI();

        //Show the custom editor
        else
        { 
            try
            {
                OnCustomInspectorGUI();
            }
            catch (EditorException ex)
            {
                base.OnInspectorGUI();
                Debug.LogError("Editor error: " + ex);
                showDefaultEditor = true;
                caughtError = ex;
            }
        }
    }

    public abstract void OnCustomInspectorGUI(); 
}
