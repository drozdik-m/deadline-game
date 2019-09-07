using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageReactionCondition))]
public class StageReactionConditionEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageReactionCondition";
    }

    public override void OnStageInspectorGUI()
    {
        DrawDefaultInspector();

        //var Target = base.Target as StageReactionCondition;

        ////Condition
        //Target.Condition = EditorGUILayout.ObjectField("Game condition", Target.Condition, typeof(Condition), true) as Condition;
        //if (Target.Condition == null)
        //    MessageBox.AddMessage("Condition is null", ErrorStyle);
        //else
        //    EditorGUILayout.LabelField("Condition is " + Target.Condition.satisfied);
    }
}
