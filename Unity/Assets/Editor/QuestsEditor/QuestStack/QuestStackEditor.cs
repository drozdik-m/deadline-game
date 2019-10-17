using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestStack))]
public class QuestStackEditor : DefaultEditor<QuestStack>
{
    ArrayEditor<QuestStack, Quest, QuestEditor> questsEditor;

    public QuestStackEditor()
    {
        questsEditor = new ArrayEditor<QuestStack, Quest, QuestEditor>("Quests", MessageBox);
    }

    public override void OnCustomInspectorGUI()
    {
        if (Target.QuestsAreCompleted())
            EditorGUILayout.LabelField("[Completed]", SuccessStyle);

        Target.FreezeOnComplete = EditorGUILayout.Toggle("Freeze once all conditions are met", Target.FreezeOnComplete);

        Target.quests = questsEditor.Use(Target);
    }
}


