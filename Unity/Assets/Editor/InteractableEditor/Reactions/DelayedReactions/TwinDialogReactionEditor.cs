using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor for Twin Dialog Reaction
/// </summary>
[CustomEditor(typeof(TwinDialogReaction))]
public class TwinDialogReactionEditor : ReactionEditor
{
    public override string GetFoldoutLabel()
    {
        return "Twin Dialog Reaction";
    }

    public override void OnCustomInspectorGUI()
    {
        TwinDialogReaction thisReaction = Target as TwinDialogReaction;

        thisReaction.delay = EditorGUILayout.Slider("Delay", thisReaction.delay, 0, 5);
        thisReaction.twinTalkDialog = (TwinTalkDialog)EditorGUILayout.ObjectField("Twin Talk Dialog",
                                                                                   thisReaction.twinTalkDialog,
                                                                                   typeof(TwinTalkDialog),
                                                                                   true);
        thisReaction.target = (GameObject)EditorGUILayout.ObjectField("Twin Talk Dialog Target",
                                                                                   thisReaction.target,
                                                                                   typeof(GameObject),
                                                                                   true);


        if (thisReaction.twinTalkDialog == null)
            MessageBox.AddMessage("Twin Talk Dialog is empty", DefaultEditor<MonoBehaviour>.WarningStyle);

        if (thisReaction.target == null)
            MessageBox.AddMessage("Twin Talk Dialog Target is empty", DefaultEditor<MonoBehaviour>.WarningStyle);
    }
}
