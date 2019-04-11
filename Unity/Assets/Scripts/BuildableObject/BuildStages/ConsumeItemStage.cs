using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Build Stage that needs item in inventory to be done and after commiting to stage, the item will be removed
/// </summary>
public class ConsumeItemStage : BuildStage
{
    /// <summary>
    /// Optional inventory to check item in
    /// </summary>
    public Inventory overrideInventory;

    /// <summary>
    /// The desired item id that player need to get through stage
    /// </summary>
    public InventoryItemID desiredItem;

    /// <summary>
    /// Checks if the conditions for going to next stage are satisfied
    /// </summary>
    /// <returns></returns>
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
        
        if (overrideInventory.CurrentItem == desiredItem)
        {
            DestroyInventoryItem();
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Destroys item in the inventory
    /// </summary>
    private void DestroyInventoryItem()
    {
        overrideInventory.DisposeCurrentItem();
    }
}
