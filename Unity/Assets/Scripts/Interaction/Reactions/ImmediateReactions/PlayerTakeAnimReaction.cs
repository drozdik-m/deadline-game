using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeAnimReaction : Reaction
{
    private Animator animator;

    protected override void SpecificInit()
    {
        base.SpecificInit();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            animator = player.GetComponentInChildren<Animator>();
        else
        {
            Debug.LogError("PlayerTakeAnimReaction - player has not been found");
            return;
        }

        if (animator == null)
            Debug.LogError("PlayerTakeAnimReaction - player animator has not been found");
    }

    protected override void ImmediateReaction()
    {
        if (animator != null)
            animator.SetBool("isInteracting", true);

    }
}
