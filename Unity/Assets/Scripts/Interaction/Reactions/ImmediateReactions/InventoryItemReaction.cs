using UnityEngine;

/// <summary>
/// Reaction that handles adding to inventory
/// </summary>
public class InventoryItemReaction : Reaction
{
    public Inventory overrideInventory;
    public InventoryItem item;

    protected override void ImmediateReaction()
    {
        if (overrideInventory == null)
            overrideInventory = GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>();

        overrideInventory.PickUp(item);
    }
}