using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ReactionEvent))]
public class ReactionEventEditor : ReactionEditor
{
    public override string GetFoldoutLabel()
    {
        return "Reaction Event";
    }

    public override void OnCustomInspectorGUI()
    {
        ReactionEvent thisReaction = Target as ReactionEvent;

        thisReaction.delay = EditorGUILayout.Slider("Delay", thisReaction.delay, 0, 5);
        thisReaction.intecationEventMiddleman = (InteractionEventMiddleman)EditorGUILayout.ObjectField("Interaction Middle Man",
                                                                                   thisReaction.intecationEventMiddleman,
                                                                                   typeof(InteractionEventMiddleman),
                                                                                   true);

        if (thisReaction.intecationEventMiddleman == null)
            MessageBox.AddMessage("Interaction Middle Man is empty", WarningStyle);
    }
}
