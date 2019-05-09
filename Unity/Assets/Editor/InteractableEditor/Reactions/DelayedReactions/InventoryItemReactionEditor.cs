using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InventoryItemReaction))]
public class InventoryItemReactionEditor : ReactionEditor
{
    OverrideMonoscriptField<Inventory> targetInventoryField =
    new OverrideMonoscriptField<Inventory>("Override Inventory", "Inventory");

    public override string GetFoldoutLabel()
    {
        return "Inventory Item Reaction";
    }

    public override void OnCustomInspectorGUI()
    {
        InventoryItemReaction thisReaction = Target as InventoryItemReaction;

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
