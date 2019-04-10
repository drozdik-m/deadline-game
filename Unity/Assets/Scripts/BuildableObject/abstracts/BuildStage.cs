using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represent one stage of the buildable object
/// </summary>
public abstract class BuildStage : ScriptableObject
{
    public GameObject gameObject;
    public abstract bool ConditionsSatisfied();

    public virtual void Init()
    {
        gameObject.SetActive(true);
    }

    public virtual void Dismiss()
    {
        gameObject.SetActive(false);
    }

}
