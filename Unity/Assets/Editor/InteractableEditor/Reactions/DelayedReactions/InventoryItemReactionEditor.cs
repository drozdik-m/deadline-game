using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor for Inventory Item Reaction
/// </summary>
[CustomEditor(typeof(InventoryItemReaction))]
public class InventoryItemReactionEditor : ReactionEditor
{
    const string MAIN_INVENTORY_TAG = "MainInventory";
    OverrideMonoscriptField<Inventory> targetInventoryField =
    new OverrideMonoscriptField<Inventory>("Override Inventory", "Inventory");

    public override string GetFoldoutLabel()
    {
        return "Inventory Item Reaction";
    }

    public override void OnCustomInspectorGUI()
    {
        InventoryItemReaction thisReaction = Target as InventoryItemReaction;

        if (thisReaction.overrideInventory == null)
        {
            GameObject mainInventoryGameObject = GameObject.FindGameObjectWithTag(MAIN_INVENTORY_TAG);
            if (mainInventoryGameObject == null)
                MessageBox.AddMessage("Game Object with tag '" + MAIN_INVENTORY_TAG + "' was not found -> Add it", ErrorStyle);

            else if (mainInventoryGameObject.GetComponent<Inventory>() == null)
                MessageBox.AddMessage("Game Object with tag '" + MAIN_INVENTORY_TAG + "' does not have Inventory component-> Add it", ErrorStyle);
        }

        thisReaction.overrideInventory = targetInventoryField.Render(thisReaction.overrideInventory);
        targetInventoryField.CheckForNullOverride(thisReaction.overrideInventory,
                                                  MessageBox, "Inventory is overriden but empty",
                                                  WarningStyle);

        thisReaction.item = (InventoryItem)EditorGUILayout.ObjectField("Inventory Item",
                                                                        thisReaction.item,
                                                                        typeof(InventoryItem),
                                                                        true);

        if (thisReaction.item == null)
            MessageBox.AddMessage("Inventory Item is empty", WarningStyle);
    }
}
