using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryItem CurrentItem = null;

    public bool PickUp(InventoryItem itemToPickUp)
    {
        if (CurrentItem != null)
            return false;

        CurrentItem = itemToPickUp;
        itemToPickUp.PickedUpBy(this);

        return true;
    }

    public void Drop()
    {
        CurrentItem.DroppedBy(this);
        CurrentItem = null;
    }

    
}
