using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEventMiddleman : MonoBehaviour
{
    public event ReactionHandler OnInteract;

    internal void IntecationTrigger(ReactionEvent reactionEvent, object args)
    {
        OnInteract?.Invoke(reactionEvent, args);
    }
}

public delegate void ReactionHandler(ReactionEvent caller, object args);
