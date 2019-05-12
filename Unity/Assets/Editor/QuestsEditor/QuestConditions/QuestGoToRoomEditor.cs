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
        //DrawDefaultInspector();

        QuestGoToRoom Target = base.Target as QuestGoToRoom;

        Target.TargetRoom = (RoomList)EditorGUILayout.EnumPopup("Target room", Target.TargetRoom);

        if (GameObject.FindGameObjectWithTag("RoomManager") == null)
            MessageBox.AddMessage("RoomManager (tag) not found", ErrorStyle);
        else if (GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>() == null)
            MessageBox.AddMessage("RoomManager found but it does not have RoomManager component", ErrorStyle);
    }
}
