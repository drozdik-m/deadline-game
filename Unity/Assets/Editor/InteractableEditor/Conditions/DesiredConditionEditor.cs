using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DesiredCondition))]
public class DesiredConditionEditor : DefaultEditor<DesiredCondition>, IArrayItemEditor
{
    public string GetFoldoutLabel()
    {
        return "Desired Condition";
    }

    public override void OnCustomInspectorGUI()
    {
        Target.condition= (Condition)EditorGUILayout.ObjectField("Condition",
                                                                  Target.condition,
                                                                  typeof(Condition),
                                                                  true);

        Target.desiredValue = EditorGUILayout.Toggle("Desired Value", Target.desiredValue);

        if (Target.condition == null)
            MessageBox.AddMessage("Condition is empty (go to 'Resources/AllConditions')", WarningStyle);
    }
}
