using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom editor for Reaction Event
/// </summary>
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

        if (thisReaction.intecationEventMiddleman == null)
        {
            EditorGUILayout.LabelField("Interaction Event Middle Man is null", WarningStyle);

            if (GUILayout.Button("Add Interaction Event Middle Man"))
            {
                InteractionEventMiddleman middleMan = thisReaction.gameObject.GetComponent<InteractionEventMiddleman>();
                if (middleMan == null) middleMan = Target.gameObject.AddComponent<InteractionEventMiddleman>();

                thisReaction.intecationEventMiddleman = middleMan;
            }

            EditorGUILayout.Space();
        }

        thisReaction.delay = EditorGUILayout.Slider("Delay", thisReaction.delay, 0, 5);
        thisReaction.intecationEventMiddleman = (InteractionEventMiddleman)EditorGUILayout.ObjectField("Interaction Middle Man",
                                                                                   thisReaction.intecationEventMiddleman,
                                                                                   typeof(InteractionEventMiddleman),
                                                                                   true);
        
    }
}
