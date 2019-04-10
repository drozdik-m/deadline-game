using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeItemStage : BuildStage
{
    public Inventory inventory;
    public InventoryItem desiredItem;

    public override bool ConditionsSatisfied()
    {
        // check if the item is in the inventory
        if (inventory == null)
            inventory = GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>();

        if (inventory == null)
        {
            Debug.Log("PersistentItemStage: Inventory is null event after trying to find it");
            return false;
        }

        if (desiredItem == inventory.CurrentItem)
        {
            DestroyInventoryItem(inventory.CurrentItem);
            return true;
        }
        else
            return false;
    }

    private void DestroyInventoryItem(InventoryItem currentItem)
    {
        throw new NotImplementedException();
    }
}
