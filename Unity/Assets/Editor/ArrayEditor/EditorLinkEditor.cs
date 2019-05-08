using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom editor for Editor Link
/// </summary>
[CustomEditor(typeof(EditorLink))]
public class EditorLinkEditor : DefaultEditor<EditorLink>, IArrayItemEditor
{
    public string GetFoldoutLabel()
    {
        return "Editor Link";
    }

    public override void OnCustomInspectorGUI()
    {
        Target.linkedGameObject = (GameObject)EditorGUILayout.ObjectField("Linked Game Object", Target.linkedGameObject, typeof(GameObject), true);

        if (Target.linkedGameObject == null)
            MessageBox.AddMessage("Linked Game Object is empty", DefaultEditor<MonoBehaviour>.WarningStyle);
    }
}
 