using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorKeeper : MonoBehaviour
{
    /// <summary>
    /// The door location (current room).
    /// </summary>
    public RoomList DoorLocation;
    /// <summary>
    /// The target location (room to go).
    /// </summary>
    public RoomList TargetLocation;
    /// <summary>
    /// The spawn position.
    /// </summary>
    public GameObject SpawnPosition;
    /// <summary>
    /// Reference to the agent.
    /// </summary>
    private NavMeshAgent agent;
    /// <summary>
    /// The reaction middleman.
    /// </summary>
    private InteractionEventMiddleman reactionMiddleman;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            agent = player.GetComponent<NavMeshAgent>();
            if (!agent)
            {
                Debug.Log("NavMeshAgent not found!(in DoorKeeper)");
            }
        }

        reactionMiddleman = GetComponent<InteractionEventMiddleman>();
      
        reactionMiddleman.OnInteract += OnInteract;

    }

    void OnInteract(ReactionEvent caller, object args)
    {
        Teleport();
    }

    /// <summary>
    /// Teleport the player to target room.
    /// </summary>
    public void Teleport()
    {
        var doors = FindObjectsOfType<DoorKeeper>();
        foreach (DoorKeeper door in doors)
        {
            if (door.DoorLocation == this.TargetLocation && door.TargetLocation == this.DoorLocation)
            {
                agent.Warp(door.SpawnPosition.transform.position);
                UpdateLocation(door.DoorLocation);
                return;
            }
        }
        Debug.Log("Door not found");
    }

    /// <summary>
    /// Updates the location.
    /// </summary>
    /// <param name="destination">Destination.</param>
    private void UpdateLocation(RoomList destination)
    {
        GameObject roomManager = GameObject.FindGameObjectWithTag("RoomManager");
        if (roomManager)
        {
            var rm = roomManager.GetComponent<RoomManager>();
            if (rm)
            {
                rm.CurrentRoom = destination;
            }
            else
            {
                Debug.Log("NavMeshAgent not found!(in DoorKeeper)");
            }
        }
    }


}
