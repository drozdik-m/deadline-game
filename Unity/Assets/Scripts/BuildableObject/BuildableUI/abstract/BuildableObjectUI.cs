using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract class that is used for implementing stage UI
/// </summary>
public abstract class BuildableObjectUI : MonoBehaviour
{
    /// <summary>
    /// Text item's state (Needed, Preparing, Completed)
    /// </summary>
    protected Text stateText;

    /// <summary>
    /// The buildable game object reference.
    /// </summary>
    protected GameObject buildableGameObject;

    /// <summary>
    /// Activates stage UI
    /// </summary>
    public abstract void Activate();

    /// <summary>
    /// Deactivates stage UI
    /// </summary>
    public abstract void Deactivate();

    /// <summary>
    /// Sets necessary values for stage UI
    /// </summary>
    /// <param name="buildableObject">Game Object that contains Buildable object</param>
    /// <param name="state">State text UI</param>
    public virtual void SetUI(SetUIArguments args)
    {
        buildableGameObject = args.BuildableGameObject;
        stateText = args.StateText;
    }

    /// <summary>
    /// Changes text state
    /// </summary>
    /// <param name="text">Text of the state</param>
    public void UpdateStateText(string text)
    {
        stateText.text = text;
    }
}
