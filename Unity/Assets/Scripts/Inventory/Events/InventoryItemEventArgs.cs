using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InventoryItem event arguments
/// </summary>
public class InventoryItemEventArgs
{
    /// <summary>
    /// Inventory that called the event
    /// </summary>
    public Inventory inventory;

    /// <summary>
    /// Drop point recommended by inventory
    /// </summary>
    public GameObject RecommendedDropPoint;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="inventory">Inventory that called or is associated with this action.</param>
    public InventoryItemEventArgs(Inventory inventory, GameObject RecommendedDropPoint)
    {
        this.inventory = inventory;
        this.RecommendedDropPoint = RecommendedDropPoint;
    }
}
