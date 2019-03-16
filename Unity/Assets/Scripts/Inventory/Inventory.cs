using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles inventory items and their storage.
/// </summary>
public class Inventory : MonoBehaviour
{
    /// <summary>
    /// Current item in the inventory
    /// </summary>
    public InventoryItem CurrentItem = null;

    /// <summary>
    /// The current inventory is empty, pick up passed item.
    /// </summary>
    /// <param name="itemToPickUp">Item that wants to be picked up</param>
    /// <returns>True if item was picked up, else false</returns>
    public bool PickUp(InventoryItem itemToPickUp)
    {
        if (CurrentItem != null)
            return false;

        CurrentItem = itemToPickUp;
        itemToPickUp.PickedUpBy(this);

        return true;
    }

    /// <summary>
    /// If any item is in the inventory, it will be dropped.
    /// </summary>
    /// <returns>True if any item was dropped, else false</returns>
    public bool Drop()
    {
        if (CurrentItem == null)
            return false;

        CurrentItem.DroppedBy(this);
        CurrentItem = null;
        return true;
    }

    
}
