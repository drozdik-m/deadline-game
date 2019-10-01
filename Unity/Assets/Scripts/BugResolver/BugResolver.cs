using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BugResolverHandler(BugResolver source, BugResolverArgs buggedItem);

public class BugResolver : MonoBehaviour
{
    /// <summary>
    /// The void treshold (When is an item considered to be falling).
    /// </summary>
    public double VoidTreshold;
    /// <summary>
    /// The drop point of the bugged item.
    /// </summary>
    private Transform dropPoint;
    /// <summary>
    /// Occurs when an item gets bugged (falls into void).
    /// </summary>
    public event BugResolverHandler OnItemBugged;

    // Start is called before the first frame update
    void Start()
    {
        var mainInventory = GameObject.FindGameObjectWithTag("MainInventory");
        if (!dropPoint)
        {
            dropPoint = mainInventory.transform.GetChild(0);
        }
        else
        {
            Debug.LogError("BugResolver: MainInvetory not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Find all items with potential risk
        var interactableGameObjects = GameObject.FindGameObjectsWithTag("CheckForBugs");

        foreach (var go in interactableGameObjects)
        {
           if (go.transform.position.y < VoidTreshold)
            {
                // Teleport the bugged item to the player
                var rb = go.GetComponent<Rigidbody>();
                // Reset its velocity
                rb.velocity = new Vector3(0, 0, 0);
                go.transform.position = dropPoint.position;
                ItemBugged(go);

            }
        }
    }
    /// <summary>
    /// Takes care of item bugged event
    /// </summary>
    /// <param name="buggedGameObject">Bugged game object.</param>
    protected virtual void ItemBugged(GameObject buggedGameObject)
    {
        OnItemBugged?.Invoke(this, new BugResolverArgs(buggedGameObject));
    }
}
