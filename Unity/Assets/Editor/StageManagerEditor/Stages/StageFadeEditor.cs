using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageFade))]
public class StageFadeEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageFade";
    }

    public override void OnStageInspectorGUI()
    {
        //DrawDefaultInspector();

        StageFade Target = base.Target as StageFade;

        //Fade color
        Target.FadeColor = EditorGUILayout.ColorField("Fading color", Target.FadeColor);

        //Fade length
        Target.FadeLength = EditorGUILayout.FloatField("Fade length [sec]", Target.FadeLength);
        if (Target.FadeLength < 0)
            MessageBox.AddMessage("Fade length is negative", ErrorStyle);

        //Fade style
        Target.FadeStyle = (StageFadeOption)EditorGUILayout.EnumPopup("Fading style", Target.FadeStyle);

    }
}
