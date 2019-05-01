using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles highlighting of interactable objects. Must be placed on game object with Interactable class.
/// </summary>
public class InteractableHighlighter : MonoBehaviour
{
    /// <summary>
    /// Override animator
    /// </summary>
    public Animator OverrideAnimator = null;

    /// <summary>
    /// Used animator
    /// </summary>
    Animator animator;
        
    /// <summary>
    /// Override collider game object
    /// </summary>
    public GameObject OverrideColliderGO = null;

    /// <summary>
    /// Used collider game object
    /// </summary>
    GameObject targetGameObject;

    /// <summary>
    /// Trigger proximity (0 = off)
    /// </summary>
    public float Proximity = 1.5f;


    private void Start()
    {
        animator = OverrideAnimator;
        if (OverrideAnimator == null)
            animator = GetComponent<Interactable>().connectedObject.GetComponent<Animator>();

        targetGameObject = OverrideColliderGO;
        if (OverrideColliderGO == null)
            targetGameObject = gameObject;
    }

    private void Update()
    {
        bool highlightOn = false;

        //CHECK HOVER
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //Did it hit this gameobject 
            if (hit.collider.gameObject == targetGameObject)
                highlightOn = true;
            else
                highlightOn = false;
        }

        //CHECK PROXIMITY
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (Proximity != 0 && Vector3.Distance(targetGameObject.transform.position, playerPosition) <= Proximity)
            highlightOn = true;


        //SET ANIMATION
        if (highlightOn)
            animator.SetBool("Highlight", true);
        else
            animator.SetBool("Highlight", false);
    }
}
