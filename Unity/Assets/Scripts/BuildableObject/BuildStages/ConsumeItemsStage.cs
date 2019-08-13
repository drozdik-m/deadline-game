using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void ConsumeItemsStageHandler(BuildStage source,ConsumeItemsStageArgs consumeItemsStageArgs);
/// <summary>
/// Invetory item and it`s count structure.
/// </summary>
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
    Dictionary<InventoryItemID, int> requiredItems = new Dictionary<InventoryItemID, int>();
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
    /// Occurs when on dictionary loaded(items were trasnfered from array).
    /// </summary>
    public event ConsumeItemsStageHandler OnDictionaryLoaded;
    /// <summary>
    /// Gets the required items in dictionary.
    /// </summary>
    /// <value>The required items in dictionary.</value>
    public Dictionary<InventoryItemID, int> RequiredItemsInDictionary 
    {
        get
        {
            return requiredItems;
        }
    }
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

        var countNeeded = 0;

        try
        {
            countNeeded = requiredItems[currentItem.ItemType];
        }
        catch
        {
            OnWrongItemTransformTry?.Invoke(this, new ConsumeItemsStageArgs(requiredItems));
            return false;
        }

        //Decrement 
        if (countNeeded > 0)
        {
            DestroyInventoryItem();
            requiredItems[currentItem.ItemType] = countNeeded - 1;
            OnItemAccepted?.Invoke(this, new ConsumeItemsStageArgs(requiredItems));
        }

        return requiredItems.All(e => e.Value <= 0);
    }

    private void Start()
    {
        TransferToDictionary();
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
    private void TransferToDictionary()
    {
        foreach (var i in RequiredItems)
        {
            requiredItems.Add(i.InvetoryItemID, i.ItemCount);
        }
        OnDictionaryLoaded?.Invoke(this, new ConsumeItemsStageArgs(requiredItems));
    }
}
