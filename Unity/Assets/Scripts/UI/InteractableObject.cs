using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for interactable items. 
/// If the player close to the item, the interactable button wil be appear.
/// </summary>
public class InteractableObject : MonoBehaviour
{
    public GameObject Player;
    /// <summary>
    /// Button, that will be appear
    /// </summary>
    public Button InteractableButton;
    /// <summary>
    /// Minimum distance between item and player, when button will be appear.
    /// </summary>
    public float distance = 100f;

    // Start is called before the first frame update
    void Start()
    {
        InteractableButton.gameObject.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!InteractableButton)
        {
            Destroy (this);
        }

        if (Vector3.Distance (Player.transform.position, transform.position) < distance)
        {
            Transform canvas = transform.GetComponentInChildren<Transform> ();
            if(InteractableButton)
                InteractableButton.gameObject.SetActive (true);
        }
        else
        {
            InteractableButton.gameObject.SetActive (false);
        }
    }
}
