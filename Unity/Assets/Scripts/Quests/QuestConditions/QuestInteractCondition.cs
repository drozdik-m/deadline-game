using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manually triggered quest condition
/// </summary>
public class QuestInteractCondition : QuestCondition
{
    public InteractionEventMiddleman InteractionEventMiddleman;

    private void Start()
    {
        InteractionEventMiddleman.OnInteract += OnInteractReaction;
    }

    private void OnInteractReaction(ReactionEvent caller, object args)
    {
        Completed = true;
    }
}
