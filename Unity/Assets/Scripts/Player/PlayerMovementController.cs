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
    /// Reffernce to the main game camera
    /// </summary>
    public Camera cam;
    /// <summary>
    /// Refference to the Player`s navMeshAgent component
    /// </summary>
    private NavMeshAgent agent;
    /// <summary>
    /// Refference to model animator
    /// </summary>
    private Animator animator;
    /// <summary>
    /// State of the player interaction
    /// </summary>
    private bool isInteracting;
    /// <summary>
    /// State of player running
    /// </summary>
    private bool isRunning;
    private Quaternion targetRotation;

    [Range(5.0f,15.0f)]
    public float rotationSpeed = 10f;

    private void Start()
    {
        targetRotation = transform.rotation;
        AllConditions.Instance.Reset();
        agent = GetComponent<NavMeshAgent>();
        cam = FindObjectOfType<Camera>();
        animator = GetComponentInChildren<Animator>();

        if (!agent)
            Debug.Log("Missing NavMeshAgent component!");
        agent.updateRotation = false;
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
        Debug.Log(":"+agent.pathPending);


    }
    private void LateUpdate()
    {
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon && isRunning)
            targetRotation = Quaternion.LookRotation(agent.velocity.normalized);

        RotateAgent(targetRotation);
    }

    void RotateAgent(Quaternion lookRotation)
    {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void HandleState()
    {
        if (agent.velocity != Vector3.zero)
            isRunning = true;

        if (agent.remainingDistance < 0.5 && isRunning )
            isRunning = false;

        // Set the animator running state
        animator.SetBool("isRunning", isRunning);

        if (!isRunning && Input.GetKeyDown("space"))
        {
            isInteracting = true;
            animator.SetBool("isInteracting", isInteracting);
        }

        isInteracting = animator.GetBool("isInteracting");
    }
    /// <summary>
    /// Moves to position.
    /// </summary>
    /// <param name="position">Position.</param>
    public void MoveToPosition(Vector3 position)
    {
        if (!isInteracting)
        agent.SetDestination(position);
    }
  


    public void OnInteractableClick(Interactable interactable)
    {
        this.MoveToPosition(interactable.interactionLocation.position);
      
        StartCoroutine(WaitUntil(interactable));
    }



    IEnumerator WaitUntil(Interactable interactable)
    {
        yield return new WaitUntil(() => !agent.pathPending && !isRunning && !agent.hasPath);

        // Sets interacting status to true
        isInteracting = true;
        targetRotation = interactable.interactionLocation.rotation;
        interactable.Interact();
        // Sets interacting status to false
        isInteracting = false;


    }

}
