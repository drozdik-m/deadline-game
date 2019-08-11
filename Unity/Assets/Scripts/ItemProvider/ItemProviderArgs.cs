
public class ItemProviderArgs
{
    /// <summary>
    /// Gets the provided item type.
    /// </summary>
    /// <value>The provided item.</value>
    public InventoryItemID ProvidedItem
    {
        get
        {
            return itemType;
        }
    }
    /// <summary>
    /// Gets a value indicating whether the item is depleeted.
    /// </summary>
    /// <value><c>true</c> if is depleeted; otherwise, <c>false</c>.</value>
    public bool IsDepleeted
    {
        get
        {
            return isDepleeted;
        }
    }
    /// <summary>
    /// Gets the remaining items count.
    /// </summary>
    /// <value>The remaining items count.</value>
    public int RemainingItemsCount
    {
        get
        {
            return remainingItemsCount;
        }
    }

    /// <summary>
    /// The type of the item.
    /// </summary>
    private readonly InventoryItemID itemType;
    /// <summary>
    /// Item is depleeted (status).
    /// </summary>
    private readonly bool isDepleeted;
    /// <summary>
    /// The number of items left.
    /// </summary>
    private readonly int remainingItemsCount;


    public ItemProviderArgs(InventoryItemID itemType, bool isDepleeted, int remainingItemsCounts )
    {
        this.itemType = itemType;
        this.isDepleeted = isDepleeted;
        this.remainingItemsCount = remainingItemsCounts;
    }
}
