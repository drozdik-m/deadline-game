using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Basic skeleton for inventory items. Not very useful alone.
/// </summary>
public class InventoryItem : MonoBehaviour
{
    /// <summary>
    /// Pick up event.
    /// </summary>
    public event InventoryItemEventHandler PickedUp;

    /// <summary>
    /// Drop event.
    /// </summary>
    public event InventoryItemEventHandler Dropped;

    /// <summary>
    /// Item type (axe, shovel, ...)
    /// </summary>
    public InventoryItemID ItemType;

    /// <summary>
    /// Item state (picked up, dropped, ...)
    /// </summary>
    public InventoryItemState itemState = InventoryItemState.Dropped;


    //--------------------------------------------------------
    //                    EVENT CALLERS 
    //--------------------------------------------------------
    /// <summary>
    /// Is generally called by an inventory. It checks item state and triggers events.
    /// </summary>
    /// <param name="inventory">Inventory that is associated with this item</param>
    public void PickedUpBy(Inventory inventory)
    {
        if (itemState == InventoryItemState.PickedUp)
            return;

        itemState = InventoryItemState.PickedUp;
        PickedUp.Invoke(inventory, new InventoryItemEventArgs(inventory));
    }

    /// <summary>
    /// Is generally called by an inventory. It checks item state and triggers events.
    /// </summary>
    /// <param name="inventory">Inventory that is associated with this item</param>
    public void DroppedBy(Inventory inventory)
    {
        if (itemState == InventoryItemState.Dropped)
            return;

        itemState = InventoryItemState.Dropped;
        Dropped.Invoke(inventory, new InventoryItemEventArgs(inventory));
    }


    //--------------------------------------------------------
    //                  COMPARE OPERATORS 
    //--------------------------------------------------------
    /// <summary>
    /// Operator overload for confortable comparison of item ID
    /// </summary>
    /// <param name="invItem">Inventory item</param>
    /// <param name="itemEnum">Inventory item ID</param>
    /// <returns>True if equal</returns>
    public static bool operator == (InventoryItem invItem, InventoryItemID itemEnum)
    {
        return invItem.ItemType == itemEnum;
    }

    /// <summary>
    /// Operator overload for confortable comparison of item ID
    /// </summary>
    /// <param name="invItem">Inventory item</param>
    /// <param name="itemEnum">Inventory item ID</param>
    /// <returns>True if equal</returns>
    public static bool operator != (InventoryItem invItem, InventoryItemID itemEnum)
    {
        return !(invItem == itemEnum);
    }


    //--------------------------------------------------------
    //                  SOME WTF METHODS 
    //--------------------------------------------------------
    /// <summary>
    /// Type equality check
    /// </summary>
    /// <param name="obj">Object to check</param>
    /// <returns>True if same object type</returns>
    public override bool Equals(object obj)
    {
        var item = obj as InventoryItem;
        return item != null &&
               base.Equals(obj) &&
               ItemType == item.ItemType;
    }

    /// <summary>
    /// Hash code generator
    /// </summary>
    /// <returns>Hash</returns>
    public override int GetHashCode()
    {
        var hashCode = -35214292;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + ItemType.GetHashCode();
        return hashCode;
    }
}
/// <summary>
/// Delegate for inventory item events
/// </summary>
/// <param name="caller">The one who called the event</param>
/// <param name="e">Arguments</param>
public delegate void InventoryItemEventHandler(object caller, InventoryItemEventArgs e);

/// <summary>
/// Various inventory IDs for eazy identification (axe, bucked etc.)
/// </summary>
public enum InventoryItemID
{
    Axe,
    Broom,
    Gun,
    Food,
    BigAssBlackDick
}

/// <summary>
/// Current item state (picked up, dropped etc.)
/// </summary>
public enum InventoryItemState
{
    PickedUp,
    Dropped,
    Unpickable
}