using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BugResolverHandler(BugResolver source, BugResolverArgs buggedItem);

public class BugResolver : MonoBehaviour
{
    /// <summary>
    /// The void treshold (When is an item considered to be falling).
    /// </summary>
    public double voidTreshold;
    /// <summary>
    /// The drop point of the bugged item.
    /// </summary>
    private Transform dropPoint;
    /// <summary>
    /// Occurs when an item gets bugged (falls into void).
    /// </summary>
    public event BugResolverHandler OnItemBugged;
    /// <summary>
    /// Occurs when player gets bugged.
    /// </summary>
    public event BugResolverHandler OnPlayerBugged;

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
        var interactableGameObjects = GameObject.FindGameObjectsWithTag("Hazardous");

        foreach (var go in interactableGameObjects)
        {
           if (go.transform.position.y < voidTreshold)
            {
                var rb = go.GetComponent<Rigidbody>();
                rb.velocity = new Vector3(0, 0, 0);
                go.transform.position = dropPoint.position;
                ItemBugged(go);

            }
        }

    }
    protected virtual void ItemBugged(GameObject buggedGameObject)
    {
        OnItemBugged?.Invoke(this, new BugResolverArgs(buggedGameObject, false));
    }

    protected virtual void PlayerBugged()
    {
        OnItemBugged?.Invoke(this, new BugResolverArgs(null, true));
    }
}
