using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stage that awaits interaction
/// </summary>
public class StageInteracted : Stage
{
    public InteractionEventMiddleman interactionEventMiddleman;
    private bool hasBeenInteractedWith = false;

    private void Start()
    {
        interactionEventMiddleman.OnInteract += OnReactionEvent;
    }

    private void OnReactionEvent(ReactionEvent caller, object args)
    {
        hasBeenInteractedWith = true;
    }

    public override bool ReadyForNextStage()
    {
        return hasBeenInteractedWith;
    }

    public override void StageEnd()
    {
        
    }

    public override void StageFixedUpdate()
    {
        
    }

    public override void StageLoad()
    {
        hasBeenInteractedWith = false;
    }

    public override void StageUpdate()
    {
       
    }
}
