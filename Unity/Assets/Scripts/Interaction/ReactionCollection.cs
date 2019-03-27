﻿using Assets.Scripts.Interaction.Abstracts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionCollection : MonoBehaviour
{
    public Reaction[] reactions = new Reaction[0];

    void Start()
    {
        for (int i = 0; i < reactions.Length; i++)
        {
            DelayedReaction delayedReaction = reactions[i] as DelayedReaction;
            if (delayedReaction)
                delayedReaction.Init();
            else
                reactions[i].Init();
        }
    }

    public void React()
    {
        for (int i = 0; i < reactions.Length; i++)
        {
            DelayedReaction delayedReaction = reactions[i] as DelayedReaction;

            if (delayedReaction)
                delayedReaction.React(this);
            else
                reactions[i].React(this);
        }
    }
}
