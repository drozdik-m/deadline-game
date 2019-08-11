using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void ConsumeItemsStageHandler(BuildStage source,ConsumeItemsStageArgs consumeItemsStageArgs);

[Serializable]
public struct InvetoryItemIDCount
{
    public InventoryItemID InvetoryItemID;
    public int ItemCount;
}

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
    /// The required items and their counts needed.
    /// </summary>
    public InvetoryItemIDCount[] RequiredItems;
    /// <summary>
    /// Desired items and their count needed in a dictionary (internal use)
    /// </summary>
    private Dictionary<InventoryItemID, int> requiredItems = new Dictionary<InventoryItemID, int>();
    /// <summary>
    /// Occurs when player tries to interact with transformer with an empty invetory.
    /// </summary>
    public event ConsumeItemsStageHandler OnEmptyInvetoryTransformTry;
    /// <summary>
    /// Occurs when on wrong item transform try.
    /// </summary>
    public event ConsumeItemsStageHandler OnWrongItemTransformTry;
    /// <summary>
    /// Occurs when the item is accepted by the transformer.
    /// </summary>
    public event ConsumeItemsStageHandler OnItemAccepted;
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

        var currentItem = overrideInventory.CurrentItem;
        if (!currentItem)
        {
            OnEmptyInvetoryTransformTry?.Invoke(this,new ConsumeItemsStageArgs(requiredItems));
            return false;
        }
        int countNeeded;

        try
        {
            countNeeded = requiredItems[currentItem.ItemType]; //Exception?
        }
        catch
        {
            OnWrongItemTransformTry?.Invoke(this, new ConsumeItemsStageArgs(requiredItems));
            return false;
        }

        //Decrement 
        if (countNeeded > 0)
        {
            Debug.Log("Accpeted");
            OnItemAccepted?.Invoke(this, new ConsumeItemsStageArgs(requiredItems));
            DestroyInventoryItem();
            requiredItems[currentItem.ItemType] = countNeeded - 1;
        }

        return requiredItems.All(e => e.Value <= 0);
    }

    private void Start()
    {
        transferToDictionary();
    }
    /// <summary>
    /// Destroys item in the inventory
    /// </summary>
    private void DestroyInventoryItem()
    {
        overrideInventory.DisposeCurrentItem();
    }
    /// <summary>
    /// Transfers items from array to dictionary.
    /// </summary>
    private void transferToDictionary()
    {
        foreach (var i in RequiredItems)
        {
            requiredItems.Add(i.InvetoryItemID, i.ItemCount);
        }
    }
}
