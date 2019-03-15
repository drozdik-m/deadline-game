using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ineractable Button in world Canvas
/// </summary>
public class InteractableButton : MonoBehaviour
{
    /// <summary>
    /// If button was pressed, it wil be destroyd.
    /// Use at the end of Animation "Pressed" 
    /// </summary>
   public void DestroyButton()
    {
        Destroy (gameObject);
    }
}
