using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageComponentActivity))]
public class StageComponentActivityEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageComponentActivity";
    }

    public override void OnStageInspectorGUI()
    {
        //DrawDefaultInspector();

        //serializedObject.Update();

        StageComponentActivity Target = base.Target as StageComponentActivity;

        //Object activity
        Target.Setting = (ObjectActivity)EditorGUILayout.EnumPopup("Enable/Disable", Target.Setting);

        //Components array
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ComponentsToHandle"), true);

        //Save serialized properties
        serializedObject.ApplyModifiedProperties();
    }
}
