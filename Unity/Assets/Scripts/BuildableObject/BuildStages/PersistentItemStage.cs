using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentItemStage : BuildStage
{
    public Inventory overrideInventory;
    public InventoryItemID desiredItem;

    public override bool ConditionsSatisfied()
    {

        // check if the item is in the inventory
        if (overrideInventory == null)
            overrideInventory = GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>();

        if (overrideInventory == null)
        {
            Debug.LogError("PersistentItemStage: Inventory is null event after trying to find it");
            return false;
        }
            
        return overrideInventory.CurrentItem == desiredItem;
    }
}
