using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Inventory))]
class InventoryEditor : DefaultEditor<Inventory>
{
    OverrideMonoscriptField<GameObject> recommendedGameObjectField
        = new OverrideMonoscriptField<GameObject>("Recommend drop target", "Drop target");

    public override void OnCustomInspectorGUI()
    {
        RequireTag("MainInventory");

        //INVENTORY ITEM
        if (Target.CurrentItem == null)
            EditorGUILayout.LabelField("No item in the inventory");
        else
            EditorGUILayout.LabelField("Current item: " + Target.CurrentItem.ItemType);

        //DROP TARGET
        Target.RecommendedDropTarget = EditorGUILayout.ObjectField("Recommended drop target", Target.RecommendedDropTarget, typeof(GameObject), true) as GameObject;
        if (Target.RecommendedDropTarget == Target.gameObject)
            MessageBox.AddMessage("Drop target is the same as inventory GameObject (should be separett)", WarningStyle);
    }
}
