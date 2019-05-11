using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CustomEditor(typeof(FaderController))]
public class FaderEditor : DefaultEditor<FaderController>
{
    public override void OnCustomInspectorGUI()
    {
        Target.defaultColor = EditorGUILayout.ColorField("Default fading color", Target.defaultColor);
        if (Target.GetComponent<Image>() == null)
            MessageBox.AddMessage("Needs to be placed with Image component", ErrorStyle);
    }
}
