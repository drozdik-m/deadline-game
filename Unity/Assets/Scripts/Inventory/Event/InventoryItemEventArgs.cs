using System.Collections;
using System.Collections.Generic;

public class InventoryItemEventArgs
{
    public Inventory inventory;

    public InventoryItemEventArgs(Inventory inventory)
    {
        this.inventory = inventory;
    }
}
