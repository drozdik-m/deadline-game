using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildableObjectUI : MonoBehaviour
{
    /// <summary>
    /// Text item's state (Needed, Preparing, Completed)
    /// </summary>
    public Text stateText;

    /// <summary>
    /// The buildable game object refference.
    /// </summary>
    public GameObject buildableGameObject;

    /// <summary>
    /// Canvas of the Transformer UI object
    /// </summary>
    public Canvas transformerUICanvas;

    public virtual void SetUI(GameObject buildableObject, Text state, Canvas canvasUI)
    {
        buildableGameObject = buildableObject;
        stateText = state;
        transformerUICanvas = canvasUI;
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
