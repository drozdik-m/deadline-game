using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayer : MonoBehaviour
{
    private void Start()
    {
    }

    public void OnInteractableClick(Interactable interactable)
    {
        interactable.Interact();
    }

}
