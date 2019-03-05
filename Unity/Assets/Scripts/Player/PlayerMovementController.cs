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
    /// 
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = FindObjectOfType<Camera>();

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
                agent.SetDestination(hit.point);
            }
        }
    }
    /// <summary>
    /// Moves to position.
    /// </summary>
    /// <param name="position">Position.</param>
    public void MoveToPosition(Vector3 position)
    {
        agent.SetDestination(position);
    }
    /// <summary>
    /// Moves to game object.
    /// </summary>
    /// <param name="targetObject">Target object.</param>
    public void MoveToGameObject(GameObject targetObject)
    {
        MoveToPosition(targetObject.transform.position);
    }
}
