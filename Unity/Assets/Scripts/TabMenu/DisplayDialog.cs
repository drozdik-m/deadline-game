using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class represent two different states for Display dialog
/// </summary>
public class DisplayDialog : MonoBehaviour
{
    /// <summary>
    /// State for true condition
    /// </summary>
    [HideInInspector]
    public bool TrueState { get; private set; }

    /// <summary>
    /// State for false condition
    /// </summary>
    [HideInInspector]
    public bool FalseState { get; private set; }

    /// <summary>
    /// Sets True state to true
    /// </summary>
    public void SetTrueState()
    {
        TrueState = true;
    }

    /// <summary>
    /// Sets False state to true
    /// </summary>
    public void SetFalseState()
    {
        FalseState = true;
    }
}
