using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestBuildObjectCondition))]
public class QuestBuildObjectConditionEditor : QuestConditionEditor
{
    public override string GetFoldoutLabel()
    {
        return "QuestBuildObjectCondition";
    }

    public override void OnConditionInspectorGUI()
    {
        //DrawDefaultInspector();

        QuestBuildObjectCondition Target = base.Target as QuestBuildObjectCondition;

        //Buildable object
        Target.BuildableObject = EditorGUILayout.ObjectField("Buildable object", Target.BuildableObject, typeof(BuildableObject), true) as BuildableObject;
        if (Target.BuildableObject == null)
            MessageBox.AddMessage("Buildable object is null", ErrorStyle);
    }
}
