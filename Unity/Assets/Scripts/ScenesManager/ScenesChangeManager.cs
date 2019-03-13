using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for changing scenes. Currently acts as a middle-layer for "Simple Fade Scene Transition System" asset.
/// </summary>
public class ScenesChangeManager : MonoBehaviour
{

    private void Start()
    {
        
    }

    /// <summary>
    /// Change scenes with default settings
    /// </summary>
    /// <param name="newSceneName">New scene name (must be in build settings)</param>
    public void ChangeScene(string newSceneName)
    {
        Initiate.Fade(newSceneName, Color.black, 1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newSceneName">New scene name (must be in build settings)</param>
    /// <param name="color">Transition color</param>
    /// <param name="multiplier">Transition speed (multiplier)</param>
    public void ChangeScene(string newSceneName, Color color, float multiplier)
    {
        Initiate.Fade(newSceneName, color, multiplier);
    }
}
