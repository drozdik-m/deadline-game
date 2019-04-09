using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionEventHandler : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void interactionCompleted()
    {
        animator.SetBool("isInteracting", false);
    }
}
