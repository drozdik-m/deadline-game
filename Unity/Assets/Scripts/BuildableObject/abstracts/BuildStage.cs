using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represent one stage of the buildable object
/// </summary>
public abstract class BuildStage : ScriptableObject
{
    public GameObject[] gameObjectsToActive;
    public GameObject[] gameObjectsToHide;

    public abstract bool ConditionsSatisfied();

    public virtual void Init()
    {
        foreach (GameObject go in gameObjectsToActive)
            go.SetActive(true);

        foreach (GameObject go in gameObjectsToHide)
            go.SetActive(false);
    }
}
