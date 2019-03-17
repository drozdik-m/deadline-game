using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterationAnimationTest : MonoBehaviour
{
    /// <summary>
    /// Refference to model animator
    /// </summary>
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if person is not running
        if (!animator.GetBool("isRunning") && Input.GetKeyDown("space"))
        {
            animator.SetBool("isInteracting", true);

            //if interacting animation has finished
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                animator.SetBool("isInteracting", false);

        }
    }
}
