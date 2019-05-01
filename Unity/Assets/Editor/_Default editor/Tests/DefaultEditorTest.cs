using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DefaultEditorTestScript))]
public class DefaultEditorTest : DefaultEditor<DefaultEditorTestScript>
{
    bool showMessages = false;

    public override void OnCustomInspectorGUI()
    {
        if (GUILayout.Button("Throw error"))
            throw new EditorException("Test exception");

        if (showMessages = GUILayout.Toggle(showMessages, "Show messages"))
        {
            MessageBox.AddMessage("This is an error", ErrorStyle);
            MessageBox.AddMessage("This is a warning", WarningStyle);
            MessageBox.AddMessage("This is a message", NormalStyle);
            MessageBox.AddMessage("This is a success", SuccessStyle);
        }
    }
}
