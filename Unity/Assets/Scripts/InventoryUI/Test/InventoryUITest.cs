using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUITest : MonoBehaviour
{
    public ClassicInventoryItem item;


    public void ChangeType()
    {
        if (item.ItemType == InventoryItemID.Food)
            item.ItemType = InventoryItemID.Gun;
        else if (item.ItemType == InventoryItemID.Gun)
            item.ItemType = InventoryItemID.Food;
    }
}
