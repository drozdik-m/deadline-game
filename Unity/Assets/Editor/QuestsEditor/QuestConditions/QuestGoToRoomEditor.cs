using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestGoToRoom))]
public class QuestGoToRoomEditor : QuestConditionEditor
{
    public override string GetFoldoutLabel()
    {
        return "QuestGoToRoom";
    }

    public override void OnConditionInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
