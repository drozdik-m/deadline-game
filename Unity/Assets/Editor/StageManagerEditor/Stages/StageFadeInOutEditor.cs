using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageFadeInOut))]
public class StageFadeInOutEditor : StageFadeAbstractEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageFadeInOut";
    }

    public override void OnFadeInspectorGUI()
    {
        //DrawDefaultInspector();

        StageFadeInOut Target = base.Target as StageFadeInOut;



        //FADE IN
        EditorGUILayout.LabelField("---FADE IN----");
        Target.FadeInColor = EditorGUILayout.ColorField("Fade in color", Target.FadeInColor);
        Target.FadeInLength = EditorGUILayout.FloatField("Fade in length [sec]", Target.FadeInLength);
        Target.FadeInDelay = EditorGUILayout.FloatField("Wait before fade out [sec]", Target.FadeInDelay);

        //FADE OUT
        EditorGUILayout.LabelField("---FADE OUT---");
        Target.FadeOutColor = EditorGUILayout.ColorField("Fade out color", Target.FadeOutColor);
        Target.FadeOutLength = EditorGUILayout.FloatField("Fade out length [sec]", Target.FadeOutLength);

        //Fade length

        if (Target.FadeInLength < 0 || Target.FadeOutLength < 0 || Target.FadeInDelay < 0)
            MessageBox.AddMessage("Fade length is negative", ErrorStyle);
    }
}
