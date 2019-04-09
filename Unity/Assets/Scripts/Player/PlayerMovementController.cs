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
    private bool isInteracting;

    private void Start()
    {
        AllConditions.Instance.Reset();
        agent = GetComponent<NavMeshAgent>();
        cam = FindObjectOfType<Camera>();
        animator = GetComponentInChildren<Animator>();

        if (!agent)
            Debug.Log("Missing NavMeshAgent component!");
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            // Calculates target position according to camera
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // If the click was on a solid object, move the agent there
                this.MoveToPosition(hit.point);
            }
        }

        if (agent.velocity != Vector3.zero)
            animator.SetBool("isRunning", true);
      
        if (agent.remainingDistance < 0.5)
            animator.SetBool("isRunning", false);

        if (!animator.GetBool("isRunning") && Input.GetKeyDown("space"))
        {
            animator.SetBool("isInteracting", true);
            isInteracting = true;
        }

        isInteracting = animator.GetBool("isInteracting");
        //Debug.Log(agent.remainingDistance);
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
    /// <summary>
    /// Moves to game object.
    /// </summary>
    /// <param name="targetObject">Target object.</param>
    public void MoveToGameObject(GameObject targetObject)
    {
        if(!isInteracting)
        MoveToPosition(targetObject.transform.position);
    }
}
