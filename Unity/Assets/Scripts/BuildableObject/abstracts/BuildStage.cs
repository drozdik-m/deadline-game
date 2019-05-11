using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represent one stage of the buildable object
/// </summary>
public abstract class BuildStage : MonoBehaviour
{
    /// <summary>
    /// Array of game objects that will be set as active (will get visible) when the build stage initializes
    /// </summary>
    public GameObject[] gameObjectsToActive;

    /// <summary>
    /// Array of game objects that will be set as NOT active (will get hidden) when the build stage initializes
    /// </summary>
    public GameObject[] gameObjectsToHide;

    /// <summary>
    /// Checks if the conditions to get into NEXT stage are satisfied
    /// </summary>
    /// <returns>True if conditions are satisfied, false if not</returns>
    public abstract bool ConditionsSatisfied();

    /// <summary>
    /// Initializes the stage
    /// </summary>
    public virtual void Init()
    {
        foreach (GameObject go in gameObjectsToActive)
            go.SetActive(true);

        foreach (GameObject go in gameObjectsToHide)
            go.SetActive(false);
    }
}
