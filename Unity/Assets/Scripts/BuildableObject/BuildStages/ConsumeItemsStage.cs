using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Build Stage that needs item in inventory to be done and after commiting to stage, the item will be removed
/// </summary>
public class ConsumeItemsStage : BuildStage
{
    /// <summary>
    /// Optional inventory to check item in
    /// </summary>
    public Inventory overrideInventory;

    /// <summary>
    /// Desired items and their count needed
    /// </summary>
    public Dictionary<InventoryItemID, int> RequiredItems;

    /// <summary>
    /// Checks if the conditions for going to next stage are satisfied
    /// </summary>
    /// <returns></returns>
    public override bool ConditionsSatisfied()
    {
        // check if the item is in the inventory
        if (overrideInventory == null)
            overrideInventory = GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>();

        //Check null inventory
        if (overrideInventory == null)
        {
            Debug.LogError("PersistentItemStage: Inventory is null event after trying to find it");
            return false;
        }

        //Try to take the item
        var currentItem = overrideInventory.CurrentItem.ItemType;
        var countNeeded = RequiredItems[currentItem]; //Exception?

        //Decrement 
        if (countNeeded > 0)
        {
            DestroyInventoryItem();
            RequiredItems[currentItem] = countNeeded - 1;
        }

        return RequiredItems.All(e => e.Value <= 0);
    }

    /// <summary>
    /// Destroys item in the inventory
    /// </summary>
    private void DestroyInventoryItem()
    {
        overrideInventory.DisposeCurrentItem();
    }
}
