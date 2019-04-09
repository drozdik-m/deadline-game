using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryChangeEventArgs
{
    public InventoryItemID newItemType;

    public InventoryChangeEventArgs(InventoryItemID newItemType)
    {
        this.newItemType = newItemType;
    }
}
