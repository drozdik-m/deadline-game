using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BuildableObjectUI : MonoBehaviour
{
    /// <summary>
    /// Text item's state (Needed, Preparing, Completed)
    /// </summary>
    public Text stateText;

    /// <summary>
    /// The buildable game object refference.
    /// </summary>
    public GameObject buildableGameObject;

    public abstract void Activate();
    public abstract void Deactivate();

    public virtual void SetUI(GameObject buildableObject, Text state)
    {
        buildableGameObject = buildableObject;
        stateText = state;
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
