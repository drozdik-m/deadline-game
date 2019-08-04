
public class ItemProviderArgs
{
    public InventoryItemID ProvidedItem
    {
        get
        {
            return itemType;
        }
    }

    private readonly InventoryItemID itemType;


    public ItemProviderArgs(InventoryItemID itemType)
    {
        this.itemType = itemType;
    }
}
