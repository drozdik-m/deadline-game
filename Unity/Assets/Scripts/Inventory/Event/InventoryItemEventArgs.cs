using System.Collections;
using System.Collections.Generic;

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
    /// Constructor.
    /// </summary>
    /// <param name="inventory">Inventory that called or is associated with this action.</param>
    public InventoryItemEventArgs(Inventory inventory)
    {
        this.inventory = inventory;
    }
}
