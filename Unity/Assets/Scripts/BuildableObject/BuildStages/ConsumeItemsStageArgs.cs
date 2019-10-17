
using System.Collections.Generic;

public class ConsumeItemsStageArgs
{
   
    /// <summary>
    /// Gets a value indicating whether the item is depleeted.
    /// </summary>
    /// <value><c>true</c> if is depleeted; otherwise, <c>false</c>.</value>
    public Dictionary<InventoryItemID,int> RequiredItems
    {
        get
        {
            return requiredItems;
        }
    }
  
    private readonly Dictionary<InventoryItemID, int> requiredItems;



    public ConsumeItemsStageArgs(Dictionary<InventoryItemID, int> requiredItems )
    {
        this.requiredItems = requiredItems;
    }
}
