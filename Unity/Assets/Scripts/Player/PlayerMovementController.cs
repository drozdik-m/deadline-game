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
    /// <summary>
    /// Rotation of player needed to look at target.
    /// </summary> 
    private Quaternion targetRotation;
    /// <summary>
    /// The rotation speed.
    /// </summary>
    [Range(5.0f,15.0f)]
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
                this.MoveToPosition(hit.point);
            }
        }

        HandleState();

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

        if (agent.remainingDistance < 0.3 && isRunning )
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
    /// <summary>
    /// Interactible response method
    /// </summary>
    /// <param name="interactable">Interactable.</param>
    public void OnInteractableClick(Interactable interactable)
    {
        isRunning = true;
        this.MoveToPosition(interactable.interactionLocation.position);
      
        StartCoroutine(WaitUntil(interactable));
    }

 
    /// <summary>
    /// Coroutine, that manages actions after finishing the path.
    /// </summary>
    /// <returns>The until.</returns>
    /// <param name="interactable">Interactable.</param>
    IEnumerator WaitUntil(Interactable interactable)
    {
        Debug.Log("adsa");
        yield return new WaitUntil(() => !agent.pathPending && !isRunning && !agent.hasPath);

        // Sets interacting status to true
        isInteracting = true;
        targetRotation = interactable.interactionLocation.rotation;
        animator.SetBool("isInteracting", isInteracting);
        interactable.Interact();
        // Sets interacting status to false
        isInteracting = false;
    }

}
