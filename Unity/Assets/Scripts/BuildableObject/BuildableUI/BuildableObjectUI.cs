using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BuildableObjectUI : MonoBehaviour
{
    /// <summary>
    /// Text item's state (Needed, Preparing, Completed)
    /// </summary>
    protected Text StateText;

    /// <summary>
    /// The buildable game object refference.
    /// </summary>
    protected GameObject BuildableGameObject;

    /// <summary>
    /// Canvas of the Transformer UI object
    /// </summary>
    protected Canvas transformerUICanvas;

    protected void Start()
    {
        transformerUICanvas = GetComponent<Canvas>();
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
    /// <param name="stateText">Text of the state(Needed, Preparing, Completed)</param>
    public void UpdateState(string stateText)
    {
        StateText.text = stateText;
    }

    public abstract void ActivateUI(GameObject buildableGameObject);

}
