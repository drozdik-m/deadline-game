using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using System;

public abstract class DefaultEditor<T> : Editor 
    where T : MonoBehaviour
{
    //Working variables
    bool showDefaultEditor = false;
    AnimBool showCaughtErrors = new AnimBool(false);
    AnimBool showPrintedErrors = new AnimBool(false);
    Exception caughtError = null;

    //Styles

    public static GUIStyle ErrorStyle
    {
        get
        {
            GUIStyle errorStyle = new GUIStyle();
            errorStyle.normal.textColor = Color.red;
            return errorStyle;
        }
    }
    public static GUIStyle WarningStyle
    {
        get
        {
            GUIStyle warningStyle = new GUIStyle();
            warningStyle.normal.textColor = Color.yellow;
            return warningStyle;
        }
    }
    public static GUIStyle NormalStyle
    {
        get
        {
            GUIStyle normalStyle = new GUIStyle();
            normalStyle.normal.textColor = Color.black;
            return normalStyle;
        }
    }
    public static GUIStyle SuccessStyle
    {
        get
        {
            GUIStyle successStyle = new GUIStyle();
            successStyle.normal.textColor = Color.green;
            return successStyle;
        }
    }
    
    //Error list
    protected EditorMessageBox MessageBox = new EditorMessageBox();

    //Target getter
    public T Target
    {
        get
        {
            return target as T;
        }
    }

    //On enable
    private void OnEnable()
    {
        showCaughtErrors.valueChanged.AddListener(Repaint);
        showPrintedErrors.valueChanged.AddListener(Repaint);
        OnCustomEnable();
    }

    //On inspector change/load etc.
    public override void OnInspectorGUI()
    {
        //Default editor toggle
        showDefaultEditor = GUILayout.Toggle(showDefaultEditor, "Show the default editor");
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        //Caught errors
        HandleCaughtErrors();

        //Printed errors
        HandlePrintedErrors();

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

    /// <summary>
    /// Shows an error if Target GameObject does not have required tag
    /// </summary>
    /// <param name="tag">Required tag</param>
    public void RequireTag(string tag)
    {
        //Check tag
        if (Target.gameObject.tag != tag)
            MessageBox.AddMessage("This GameObject should have tag \"" + tag + "\"", ErrorStyle);
    }

    /// <summary>
    /// Handles caught errors
    /// </summary>
    void HandleCaughtErrors()
    {
        //Show caught error
        showCaughtErrors.target = caughtError != null;
        if (EditorGUILayout.BeginFadeGroup(showCaughtErrors.faded))
        {
            EditorGUILayout.BeginHorizontal();

            string errorMessage = caughtError == null ? "Error cleared" : "Caught error: " + caughtError.Message;
            GUIStyle style = caughtError == null ? SuccessStyle : ErrorStyle;
            EditorGUILayout.LabelField(errorMessage, style);

            if (GUILayout.Button("Clear"))
                caughtError = null;

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }
        EditorGUILayout.EndFadeGroup();
    }

    /// <summary>
    /// Handles printed errors
    /// </summary>
    void HandlePrintedErrors()
    {
        //Show caught error
        showPrintedErrors.target = !MessageBox.IsEmpty();
        if (EditorGUILayout.BeginFadeGroup(showPrintedErrors.faded))
        {
            MessageBox.WriteMessages();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }
        EditorGUILayout.EndFadeGroup();

    }
    
    /// <summary>
    /// Method called by DefaultEditor if custom editor is turned on
    /// </summary>
    public abstract void OnCustomInspectorGUI();

    /// <summary>
    /// Method called by DefaultEditor on enable
    /// </summary>
    public void OnCustomEnable()
    {

    }
}
