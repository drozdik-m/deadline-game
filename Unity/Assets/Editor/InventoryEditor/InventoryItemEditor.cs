using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

abstract class InventoryItemEditor<T> : DefaultEditor<T>
    where T : InventoryItem
{
    OverrideMonoscriptField<GameObject> recommendedGameObjectField
        = new OverrideMonoscriptField<GameObject>("Recommend drop target", "Drop target");

    protected void CreateInventoryItemEditor()
    {
        //Item type
        Target.ItemType = (InventoryItemID)EditorGUILayout.EnumPopup("Item type", Target.ItemType);

        //State
        EditorGUILayout.LabelField("State: " + Target.ItemState);
    }
}
