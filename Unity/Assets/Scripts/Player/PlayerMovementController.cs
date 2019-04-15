using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// Player movement controller
/// </summary>
public class PlayerMovementController : MonoBehaviour
{
    /// <summary>
    /// Reffernce to the main game camera.
    /// </summary>
    private Camera cam;
    /// <summary>
    /// Refference to the Player`s navMeshAgent component.
    /// </summary>
    private NavMeshAgent agent;
    /// <summary>
    /// Refference to model animator.
    /// </summary>
    private Animator animator;
    /// <summary>
    /// State of the player interaction.
    /// </summary>
    private bool isInteracting;
    /// <summary>
    /// State of player running.
    /// </summary>
    private bool isRunning;
    /// <summary>
    /// State of the playermovement
    /// </summary>
    private bool isDisabled;
    public float MinimalDistance = 0.4f;
    /// <summary>
    /// Rotation of player needed to look at target.
    /// </summary> 
    private Quaternion targetRotation;
    /// <summary>
    /// The proximity movement.
    /// </summary>
    bool proximityMovement = false;
    /// <summary>
    /// The proximity movement distance.
    /// </summary>
    float proximityMovementDistance;
    /// <summary>
    /// The proximity target.
    /// </summary>
    Vector3 proximityTarget = Vector3.forward;
    /// <summary>
    /// The rotation speed.
    /// </summary>
    [Range(5.0f, 15.0f)]
    public float rotationSpeed = 10f;

    private void Start()
    {
        // Set the default rotation (no rotation at all)
        targetRotation = transform.rotation;
        AllConditions.Instance.Reset();
        agent = GetComponent<NavMeshAgent>();
        cam = FindObjectOfType<Camera>();
        animator = GetComponentInChildren<Animator>();
        // Do not rotate the agent
        agent.updateRotation = false;

        if (!agent)
            Debug.Log("Missing NavMeshAgent component!");

        if (!cam)
            Debug.Log("Missing Camera object in scene!");

        if (!animator)
            Debug.Log("Missing animator!");

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            // Calculates target position according to camera
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Ground")
            {
                // If the click was on a solid object, move the agent there
                if (Vector3.Distance(gameObject.transform.position, hit.point) > MinimalDistance)
                this.MoveToPosition(hit.point);
            }
        }

        HandleState();

        HandleProximityUpdate();
    }

    private void HandleProximityUpdate()
    {
        if (!proximityMovement)
            return;

        if (Vector3.Distance(gameObject.transform.position, proximityTarget) < proximityMovementDistance)
            StopMoving();
    }

    private void LateUpdate()
    {
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon && isRunning)
            targetRotation = Quaternion.LookRotation(agent.velocity.normalized);

        RotateAgent(targetRotation);
    }
    /// <summary>
    /// Rotates the agent.
    /// </summary>
    /// <param name="lookRotation">Look rotation.</param>
    void RotateAgent(Quaternion lookRotation)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
    public void SetState(bool state)
    {
        isDisabled = state;
    }

    /// <summary>
    /// Handles animator and states, updates them.
    /// </summary>
    void HandleState()
    {
        if (agent.velocity != Vector3.zero)
            isRunning = true;

        if (agent.remainingDistance < 0.3f && isRunning)
            isRunning = false;

        // Set the animator running state
        animator.SetBool("isRunning", isRunning);

        isInteracting = animator.GetBool("isInteracting");
    }
    /// <summary>
    /// Moves to position.
    /// </summary>
    /// <param name="position">Position.</param>
    public void MoveToPosition(Vector3 position)
    {
        if (!isInteracting && !isDisabled)
            agent.SetDestination(position);
    }

    public void MoveToPosition(Vector3 position, float proximity)
    {
        MoveToPosition(position);
        SetProximityFlags(position, proximity);
    }


    /// <summary>
    /// Stop the player from moving (cancels current agent path)
    /// </summary>
    public void StopMoving()
    {
        agent.ResetPath();
        isRunning = false;
    }

 
    private void SetProximityFlags(Vector3 target, float proximity)
    {
        proximityMovement = true;
        proximityMovementDistance = proximity;
        proximityTarget = target;
    }

    /// <summary>
    /// Interactible response method
    /// </summary>
    /// <param name="interactable">Interactable.</param>
    public void OnInteractableClick(Interactable interactable)
    {
        isRunning = true;

        if (interactable.useProximity)
        {
            this.MoveToPosition(interactable.interactionLocation.position, interactable.proximity);
        }
        else
        {
            this.MoveToPosition(interactable.interactionLocation.position);
        }

        StartCoroutine(WaitUntil(interactable));
    }

    public void OnInteractableClick(Interactable interactable, float proximity)
    {
        isRunning = true;
        this.MoveToPosition(interactable.interactionLocation.position, proximity);

        StartCoroutine(WaitUntil(interactable));

    }


    /// <summary>
    /// Coroutine, that manages actions after finishing the path.
    /// </summary>
    /// <returns>The until.</returns>
    /// <param name="interactable">Interactable.</param>
    IEnumerator WaitUntil(Interactable interactable)
    {
        yield return new WaitUntil(() => !agent.pathPending && !isRunning && !agent.hasPath);

        // Sets interacting status to true
        isInteracting = true;
        if (!proximityMovement)
            targetRotation = interactable.interactionLocation.rotation;
        proximityMovement = false;
        interactable.Interact();
        // Sets interacting status to false
       // isInteracting = false;
    }

}
