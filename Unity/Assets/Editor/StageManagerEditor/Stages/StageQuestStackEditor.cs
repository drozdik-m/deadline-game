using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageQuestStack))]
public class StageQuestStackEditor : StageEditor
{
    public override string GetFoldoutLabel()
    {
        return "StageQuestStack";
    }

    public override void OnStageInspectorGUI()
    {
        StageQuestStack Target = base.Target as StageQuestStack;

        //Set quest stack
        Target.QuestStack = EditorGUILayout.ObjectField("Quest Stack", Target.QuestStack, typeof(QuestStack), true) as QuestStack;

        if (Target.QuestStack == null)
            Target.QuestStack = Target.gameObject.GetComponent<QuestStack>();

        if (Target.QuestStack == null/* && Target.gameObject.GetComponent<QuestStack>() == null*/)
        {
            MessageBox.AddMessage("QuestStack is null", ErrorStyle);
            if (GUILayout.Button("Add QuestStack component"))
                Target.gameObject.AddComponent<QuestStack>();
        }
    }
}
