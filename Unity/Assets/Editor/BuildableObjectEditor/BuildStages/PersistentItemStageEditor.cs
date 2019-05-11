using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor for Persistem Item Stage Editor
/// </summary>
[CustomEditor(typeof(PersistentItemStage))]
public class PersistentItemStageEditor : BuildStageEditor
{

    const string MAIN_INVENTORY_TAG = "MainInventory";
    OverrideMonoscriptField<Inventory> targetInventoryField =
        new OverrideMonoscriptField<Inventory>("Override Inventory", "Inventory");

    public override string GetFoldoutLabel()
    {
        return "Persistent Item Stage";
    }

    protected override void OnBuildStageInspectorGUI()
    {
        PersistentItemStage thisBuildStage = Target as PersistentItemStage;

        if (thisBuildStage.overrideInventory == null)
        {
            GameObject mainInventoryGameObject = GameObject.FindGameObjectWithTag(MAIN_INVENTORY_TAG);
            if (mainInventoryGameObject == null)
                MessageBox.AddMessage("Game Object with tag '" + MAIN_INVENTORY_TAG + "' was not found -> Add it", ErrorStyle);

            else if (mainInventoryGameObject.GetComponent<Inventory>() == null)
                MessageBox.AddMessage("Game Object with tag '" + MAIN_INVENTORY_TAG + "' does not have Inventory component-> Add it", ErrorStyle);
        }

        thisBuildStage.overrideInventory = targetInventoryField.Render(thisBuildStage.overrideInventory);
        targetInventoryField.CheckForNullOverride(thisBuildStage.overrideInventory,
                                                  MessageBox, "Inventory is overriden but empty",
                                                  WarningStyle);

        thisBuildStage.desiredItem = (InventoryItemID)EditorGUILayout.EnumPopup("Desired Item", thisBuildStage.desiredItem);
    }
}
