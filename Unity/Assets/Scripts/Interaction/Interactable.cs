using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interaction
{
    public class Interactable : MonoBehaviour
    {
        public Transform interactionLocation;
        public ConditionCollection[] conditionCollections =
            new ConditionCollection[0];
        public ReactionCollection defaultReactionCollection;

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
}
