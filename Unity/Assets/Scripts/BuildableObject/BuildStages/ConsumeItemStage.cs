using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeItemStage : BuildStage
{
    public Inventory overrideInventory;
    public InventoryItemID desiredItem;

    public override bool ConditionsSatisfied()
    {
        Debug.Log("Conditions satisfied in ConsumeItemStage");
        // check if the item is in the inventory
        if (overrideInventory == null)
            overrideInventory = GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>();

        if (overrideInventory == null)
        {
            Debug.LogError("PersistentItemStage: Inventory is null event after trying to find it");
            return false;
        }
        
        if (overrideInventory.CurrentItem == desiredItem)
        {
            DestroyInventoryItem();
            return true;
        }
        else
            return false;
    }

    private void DestroyInventoryItem()
    {
        Debug.Log("ConsumeItemStage.DestroyInventoryItem() called");
        overrideInventory.DisposeCurrentItem();
    }
}
