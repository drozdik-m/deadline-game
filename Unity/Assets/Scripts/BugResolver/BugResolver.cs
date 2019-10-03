using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bug resolver manager
/// </summary>
public class BugResolver : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Find all items with potential risk
        var interactableGameObjects = FindObjectsOfType<BugResolverSituation>();
        // Iterate thru all gameobjects with bugresolversituation component
        foreach (var go in interactableGameObjects)
            go.Protect();
    }
}
