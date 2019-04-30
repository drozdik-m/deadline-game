using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FaderController))]
public class FaderEditor : DefaultEditor<FaderController>
{
    public override void OnCustomInspectorGUI()
    {
        Target.defaultColor = EditorGUILayout.ColorField("Default fading color", Target.defaultColor);
        EditorGUILayout.LabelField("Needs to be placed with Image component");
    }
}
