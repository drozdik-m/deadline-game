using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles highlighting of interactable objects. Must be placed on game object with Interactable class.
/// </summary>
public class InteractableHighlighter : MonoBehaviour
{
    /// <summary>
    /// Used animator
    /// </summary>
    public Animator Animator = null;
        
    /// <summary>
    /// Collider game object
    /// </summary>
    public GameObject TargetGameObject = null;

    /// <summary>
    /// Trigger proximity (0 = off)
    /// </summary>
    public float Proximity = 1.5f;

    private void Start()
    {
        if (Animator == null)
            Animator = GetComponent<Interactable>().connectedObject.GetComponent<Animator>();

        if (TargetGameObject == null)
            TargetGameObject = gameObject;
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
            if (hit.collider.gameObject == TargetGameObject)
                highlightOn = true;
            else
                highlightOn = false;
        }

        //CHECK PROXIMITY
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (Proximity != 0 && Vector3.Distance(TargetGameObject.transform.position, playerPosition) <= Proximity)
            highlightOn = true;


        //SET ANIMATION
        if (highlightOn)
            Animator.SetBool("Highlight", true);
        else
            Animator.SetBool("Highlight", false);
    }
}
