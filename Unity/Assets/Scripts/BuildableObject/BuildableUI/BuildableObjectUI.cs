using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildableObjectUI : MonoBehaviour
{
    /// <summary>
    /// Text item's state (Needed, Preparing, Completed)
    /// </summary>
    protected Text stateText;

    /// <summary>
    /// The buildable game object refference.
    /// </summary>
    protected GameObject buildableGameObject;

    /// <summary>
    /// Canvas of the Transformer UI object
    /// </summary>
    protected Canvas transformerUICanvas;

    public BuildableObjectUI(GameObject buildableObject, Text state, Canvas canvasUI)
    {
        buildableGameObject = buildableObject;
        stateText = state;
        transformerUICanvas = canvasUI;
    }

    /// <summary>
    /// Opens Transformer UI
    /// </summary>
    public void OpenUIDialog()
    {
        transformerUICanvas.enabled = true;
    }

    /// <summary>
    /// Closes Transformer UI
    /// </summary>
    public void CloseUIDialog()
    {
        transformerUICanvas.enabled = false;
    }

    /// <summary>
    /// Changes text state
    /// </summary>
    /// <param name="text">Text of the state</param>
    public void UpdateState(string text)
    {
        stateText.text = text;
    }
}
