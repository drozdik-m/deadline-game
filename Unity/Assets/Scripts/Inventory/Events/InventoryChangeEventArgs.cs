using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryChangeEventArgs : MonoBehaviour
{
    public InventoryItemID newItemType;

    public InventoryChangeEventArgs(InventoryItemID newItemType)
    {
        this.newItemType = newItemType;
    }
}
