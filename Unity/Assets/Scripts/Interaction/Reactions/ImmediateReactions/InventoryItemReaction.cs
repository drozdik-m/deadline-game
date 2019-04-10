using UnityEngine;

/// <summary>
/// Reaction that handles adding to inventory
/// </summary>
public class InventoryItemReaction : Reaction
{
    public Inventory inventory;
    public InventoryItem item;

    protected override void ImmediateReaction()
    {
        if (inventory == null)
            inventory = GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>();

        inventory.PickUp(item);
    }
}