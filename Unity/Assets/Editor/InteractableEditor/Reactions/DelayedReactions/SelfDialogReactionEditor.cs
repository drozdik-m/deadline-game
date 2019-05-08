using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor for Self Dialog Reaction
/// </summary>
[CustomEditor(typeof(SelfDialogReaction))]
public class SelfDialogReactionEditor : ReactionEditor
{
    public override string GetFoldoutLabel()
    {
        return "Self Dialog Reaction";
    }

    public override void OnCustomInspectorGUI()
    {
        SelfDialogReaction thisReaction = Target as SelfDialogReaction;

        thisReaction.delay = EditorGUILayout.Slider("Delay", thisReaction.delay, 0, 5);
        thisReaction.selfTalkDialog = (SelfTalkDialog)EditorGUILayout.ObjectField("Self Talk Dialog",
                                                                                   thisReaction.selfTalkDialog,
                                                                                   typeof(SelfTalkDialog),
                                                                                   true);

        if (thisReaction.selfTalkDialog == null)
            MessageBox.AddMessage("Self Talk Dialog is empty", DefaultEditor<MonoBehaviour>.WarningStyle);
    }
}
