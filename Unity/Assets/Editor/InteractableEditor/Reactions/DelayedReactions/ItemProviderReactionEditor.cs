using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom Editor for Item Provider Reaction
/// </summary>
[CustomEditor(typeof(ItemProviderReaction))]
public class ItemProviderReactionEditor : ReactionEditor
{
    private GameObject GetInteractableParent()
    {
        GameObject currObject = Target.gameObject;
        
        while (currObject != null)
        { 
            if (currObject.GetComponent<Interactable>())
                return currObject;

            currObject = currObject.transform.parent.gameObject;
        }

        return null;
    }

    public override string GetFoldoutLabel()
    {
        return "Item Provider Reaction";
    }

    public override void OnCustomInspectorGUI()
    {
        ItemProviderReaction thisReaction = Target as ItemProviderReaction;

        thisReaction.delay = EditorGUILayout.Slider("Delay", thisReaction.delay, 0, 5);

        thisReaction.itemProvider = (ItemProvider)EditorGUILayout
            .ObjectField("Item Provider",
                         thisReaction.itemProvider,
                         typeof(ItemProvider),
                         true);

        // try to find item provider component on interactable parent
        GameObject interactableParent = GetInteractableParent();
        if (interactableParent != null)
        {
            ItemProvider attachedItemProvider = interactableParent.GetComponent<ItemProvider>();
            if (attachedItemProvider != null)
                thisReaction.itemProvider = attachedItemProvider;
            else
                MessageBox.AddMessage("No Item Provider Component on interactable parent", WarningStyle);
        }    
        else
            MessageBox.AddMessage("No parent with interactable component found", WarningStyle);

        if (thisReaction.itemProvider == null)
            MessageBox.AddMessage("Item Provider is empty", WarningStyle);
    }
}
