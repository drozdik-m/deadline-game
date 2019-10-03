using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ProtectFloorDropping : BugResolverSituation
{
    /// <summary>
    /// The void treshold (When is an item considered to be falling).
    /// </summary>
    public double VoidTreshold;
    /// <summary>
    /// The drop point of the bugged item.
    /// </summary>
    private Transform dropPoint;

    void Start()
    {
        var mainInventory = GameObject.FindGameObjectWithTag("MainInventory");
        if (!dropPoint)
        {
            dropPoint = mainInventory.transform.GetChild(0);
        }
        else
        {
            Debug.LogError("ProtectFloorDropping: MainInvetory not found");
        }
    }

    public override bool Protect()
    {
        if (gameObject.transform.position.y < VoidTreshold)
        {
            // Teleport the bugged item to the player
            var rb = gameObject.GetComponent<Rigidbody>();
            // Reset its velocity
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.transform.position = dropPoint.position;
            return true;
        }
        return false;
    }

}
