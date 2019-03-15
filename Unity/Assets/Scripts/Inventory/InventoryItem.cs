using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryItem : MonoBehaviour
{
    public event InventoryItemEventHandler PickedUp;
    public event InventoryItemEventHandler Dropped;
    public InventoryItemID ItemType;

    //--------------------------------------------------------
    //                    EVENT CALLERS 
    //--------------------------------------------------------
    public void PickedUpBy(Inventory inventory)
    {
        PickedUp.Invoke(this, new InventoryItemEventArgs(inventory));
    }

    public void DroppedBy(Inventory inventory)
    {
        PickedUp.Invoke(this, new InventoryItemEventArgs(inventory));
    }


    //--------------------------------------------------------
    //                  COMPARE OPERATORS 
    //--------------------------------------------------------
    public static bool operator == (InventoryItem invItem, InventoryItemID itemEnum)
    {
        return invItem.ItemType == itemEnum;
    }

    public static bool operator != (InventoryItem invItem, InventoryItemID itemEnum)
    {
        return !(invItem == itemEnum);
    }


    //--------------------------------------------------------
    //                  SOME WTF METHODS 
    //--------------------------------------------------------
    public override bool Equals(object obj)
    {
        var item = obj as InventoryItem;
        return item != null &&
               base.Equals(obj) &&
               ItemType == item.ItemType;
    }

    public override int GetHashCode()
    {
        var hashCode = -35214292;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + ItemType.GetHashCode();
        return hashCode;
    }
}

public delegate void InventoryItemEventHandler(object sender, InventoryItemEventArgs e);

public enum InventoryItemID
{
    Axe,
    Broom,
    Gun,
    Food,
    BigAssBlackDick
}
