using System;
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
    /// Event called on inventory item change
    /// </summary>
    public event InventoryChangeEventHandler OnChange;

    /// <summary>
    /// The current inventory is empty, pick up passed item.
    /// </summary>
    /// <param name="itemToPickUp">Item that wants to be picked up</param>
    /// <returns>True if item was picked up, else false</returns>
    public bool PickUp(InventoryItem itemToPickUp)
    {
        if (CurrentItem != null)
            return false;

        //Change current item
        CurrentItem = itemToPickUp;
        itemToPickUp.PickedUpBy(this);

        //Event
        OnChange?.Invoke(this, new InventoryChangeEventArgs(CurrentItem.ItemType));

        return true;
    }

    /// <summary>
    /// Disposes currently equiped item.
    /// </summary>
    public void DisposeCurrentItem()
    {
        if (CurrentItem == null)
            return;

        Destroy(CurrentItem.gameObject);
        CurrentItem = null;

        OnChange?.Invoke(this, new InventoryChangeEventArgs(InventoryItemID.None));
    }

    /// <summary>
    /// If any item is in the inventory, it will be dropped.
    /// </summary>
    /// <returns>True if any item was dropped, else false</returns>
    public bool Drop()
    {
        if (CurrentItem == null)
            return false;

        //Change current item
        CurrentItem.DroppedBy(this);
        CurrentItem = null;

        //Event
        OnChange?.Invoke(this, new InventoryChangeEventArgs(InventoryItemID.None));

        return true;
    }
}


/// <summary>
/// Delegate for inventory change events
/// </summary>
/// <param name="caller">The one who called the event</param>
/// <param name="e">Arguments</param>
public delegate void InventoryChangeEventHandler(Inventory caller, InventoryChangeEventArgs e);