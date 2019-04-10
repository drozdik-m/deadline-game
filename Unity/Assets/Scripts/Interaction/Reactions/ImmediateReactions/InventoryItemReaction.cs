using UnityEngine;

/// <summary>
/// Debug reaction is debugging tool for testing reactions
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