using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper for easy public inspector access
/// </summary>
public class InventoryInspectorHelper : MonoBehaviour
{
    /// <summary>
    /// Inventory that will manage procedures
    /// </summary>
    public Inventory inventory = null;

    /// <summary>
    /// Inventory item to be picked up/dropped
    /// </summary>
    public InventoryItem thisInventoryItem = null;

    private void Start()
    {
        if (thisInventoryItem == null)
            thisInventoryItem = GetComponent<InventoryItem>();

        if (inventory == null)
            inventory = GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>();
    }

    /// <summary>
    /// Picks the item up
    /// </summary>
    /// <returns></returns>
    public bool PickUpThis()
    {
        return inventory.PickUp(thisInventoryItem);
    }

    /// <summary>
    /// Drops the item
    /// </summary>
    /// <returns></returns>
    public bool DropThis()
    {
        return inventory.Drop();
    }
}
