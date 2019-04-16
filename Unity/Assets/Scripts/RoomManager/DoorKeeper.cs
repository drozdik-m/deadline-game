using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorKeeper : MonoBehaviour
{
    public RoomList DoorLocation;
    public RoomList TargetLocation;
    public GameObject SpawnPosition;
    private NavMeshAgent agent;
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
        Debug.Log("teleport");
        Teleport();
    }


    private void Update()
    {
        if (Input.GetKeyDown("escape"))
            Teleport();
    }

    public void Teleport()
    {
        var doors = FindObjectsOfType<DoorKeeper>();
        foreach (DoorKeeper door in doors)
        {
            if (door.DoorLocation == this.TargetLocation)
            {
                Debug.Log("Teleporting");
                agent.Warp(door.SpawnPosition.transform.position);
                UpdateLocation(door.DoorLocation);
                return;
            }

            Debug.Log("Door not found");

        }
    }

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
