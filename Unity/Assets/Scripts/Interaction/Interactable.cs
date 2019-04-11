using UnityEngine;

/// <summary>
/// Interactable object represents interactive item in the game.
/// </summary>
public class Interactable : MonoBehaviour
{
    private void Start()
    {
        if (useProximity)
        {
            Transform connectedGameObjectTransform = connectedObject.GetComponent<Transform>();
            interactionLocation = connectedGameObjectTransform.transform;
        }
    }

    /// <summary>
    /// interaction location sets the position for player to walk to before interacting
    /// </summary>
    public Transform interactionLocation;

    /// <summary>
    /// Game object from where the proximity will be counted
    /// </summary>
    public GameObject connectedObject;

    /// <summary>
    /// Idicates whether interactable uses interactable location
    /// or proximty
    /// </summary>
    public bool useProximity;

    /// <summary>
    /// How wide the proximity will be
    /// </summary>
    public float proximity;

    /// <summary>
    /// condition collections for conditional interaction
    /// </summary>
    public ConditionCollection[] conditionCollections = new ConditionCollection[0];

    /// <summary>
    /// default reaction collection is used when no condition collection conditions are met
    /// </summary>
    public ReactionCollection defaultReactionCollection;

    /// <summary>
    /// perform the interaction
    /// </summary>
    public void Interact()
    {
        for (int i = 0; i < conditionCollections.Length; i++)
        {
            // if one of the collections react, we don't want
            // the default reaction to be done
            if (conditionCollections[i].CheckAndReact())
                return;
        }

        defaultReactionCollection.React();
    }
}
