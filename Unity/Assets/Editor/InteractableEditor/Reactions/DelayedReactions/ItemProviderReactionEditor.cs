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

        if (thisReaction.itemProvider == null)
        {
            if (GUILayout.Button("Add Item Provider"))
            {
                GameObject interactableParent = GetInteractableParent();
                interactableParent.AddComponent<ItemProvider>();
                thisReaction.itemProvider = interactableParent.GetComponent<ItemProvider>();
            }

            MessageBox.AddMessage("Item Provider is empty", WarningStyle);
        }

        thisReaction.delay = EditorGUILayout.Slider("Delay", thisReaction.delay, 0, 5);

        thisReaction.itemProvider = (ItemProvider)EditorGUILayout
            .ObjectField("Item Provider",
                         thisReaction.itemProvider,
                         typeof(ItemProvider),
                         true);
    }
}
