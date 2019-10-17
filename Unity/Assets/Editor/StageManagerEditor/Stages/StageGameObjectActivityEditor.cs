using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageGameObjectActivity))]
public class StageGameObjectActivityEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageGameObjectActivity";
    }

    public override void OnStageInspectorGUI()
    {
        //DrawDefaultInspector();

        StageGameObjectActivity Target = base.Target as StageGameObjectActivity;

        //Object activity
        Target.Setting = (ObjectActivity)EditorGUILayout.EnumPopup("Enable/Disable", Target.Setting);

        //Components array
        EditorGUILayout.PropertyField(serializedObject.FindProperty("GameObjectsToHandle"), true);

        //Save serialized properties
        serializedObject.ApplyModifiedProperties();

        if (Target.GameObjectsToHandle.Length == 0)
            MessageBox.AddMessage("There are no GameObjects to handle. This stage is useless.", WarningStyle);
    }
}
