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
        GameObject dialogManagerObj = GameObject.FindGameObjectWithTag("DialogManager");

        if (dialogManagerObj == null)
            MessageBox.AddMessage("Dialog Manager object was not found -> Add it", ErrorStyle);

        else if (dialogManagerObj.GetComponent<DialogManager>() == null)
        {
            EditorGUILayout.LabelField("Dialog Manager object does not have Dialog Manager component", WarningStyle);
            if (GUILayout.Button("Add Dialog Manager Component"))
                dialogManagerObj.AddComponent<DialogManager>();
        }

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
            MessageBox.AddMessage("Twin Talk Dialog is empty (go to 'Resources/Dialogs')", WarningStyle);

        if (thisReaction.target == null)
            MessageBox.AddMessage("Twin Talk Dialog Target is empty", WarningStyle);

        

    }
}
