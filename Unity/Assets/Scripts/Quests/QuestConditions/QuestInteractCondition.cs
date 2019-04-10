using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manually triggered quest condition
/// </summary>
public class QuestInteractCondition : QuestManualCondition
{
    public InteractionEventMiddleman interactionEventMiddleman;

    private void Start()
    {
        interactionEventMiddleman.OnInteract += OnInteractReaction;
    }

    private void OnInteractReaction(ReactionEvent caller, object args)
    {
        Completed = true;
    }
}
