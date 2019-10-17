using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BugResolverHandler(BugResolver source, BugResolverArgs buggedObject);

/// <summary>
/// Bug resolver manager
/// </summary>
public class BugResolver : MonoBehaviour
{
    /// <summary>
    /// Occurs when on item bugged.
    /// </summary>
    public event BugResolverHandler OnObjectBugged;
    // Update is called once per frame
    void Update()
    {
        // Find all items with potential risk
        var interactableGameObjects = FindObjectsOfType<BugResolverSituation>();
        // Iterate thru all gameobjects with bugresolversituation component
        foreach (var go in interactableGameObjects)
            if (go.Protect())
                ItemBugged(go.gameObject);
    }
    /// <summary>
    /// Object got bugged
    /// </summary>
    /// <param name="buggedGameObject">Bugged game object.</param>
    protected virtual void ItemBugged(GameObject buggedGameObject)
    {
        OnObjectBugged?.Invoke(this, new BugResolverArgs(buggedGameObject));
    }
}
