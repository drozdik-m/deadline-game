﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CustomEditor(typeof(FaderController))]
public class FaderEditor : DefaultEditor<FaderController>
{
    public override void OnCustomInspectorGUI()
    {
        //Set color
        Target.defaultColor = EditorGUILayout.ColorField("Default fading color", Target.defaultColor);

        //Check image component
        if (Target.GetComponent<Image>() == null)
            MessageBox.AddMessage("Needs to be placed with Image component", ErrorStyle);

        //Check tag
        if (Target.gameObject.tag != "Fader")
            MessageBox.AddMessage("GameObject should have tag \"Fader\"", ErrorStyle);
    }
}
