using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Class controls elements of UI inventory
/// </summary>
public class InventoryUIController : MonoBehaviour
{
    /// <summary>
    /// Actual inventory
    /// </summary>
    public Inventory Inventory;

    /// <summary>
    /// Image for the plane in the UI
    /// </summary>
    public Image ActualItemsImage;

    /// <summary>
    /// Contains type of the item in the inventory
    /// </summary>
    public Text ItemsDescription;

    /// <summary>
    /// Stores all images for all items
    /// </summary>
    public Sprite[] SpritesStore;

    /// <summary>
    /// Status of inventory
    /// </summary>
    private bool isInventoryEmpty;

    // Start is called before the first frame update
    void Start()
    {
        Inventory.OnChange += OnChange;
        ActualItemsImage.sprite = null;
        ItemsDescription.text = "";
        isInventoryEmpty = true;
    }

    /// <summary>
    /// Change UI inventory status, depends if in the
    /// inventory was item or not
    /// </summary>
    /// <param name="inv">Inventory where the method was called from</param>
    /// <param name="item">New item in the inventory</param>
    public void OnChange(Inventory inv, InventoryChangeEventArgs item)
    {
        if (item.newItemType == InventoryItemID.None)
        {
            DropItem ();
            isInventoryEmpty = true;
        } else if (isInventoryEmpty)
        {
            PickUpItem (item);
            isInventoryEmpty = false;
        }
    }

    /// <summary>
    /// Look for the new image for UI item
    /// </summary>
    /// <param name="item">Type of the item</param>
    /// <returns>New image</returns>
    private Sprite chooseSprite(InventoryItemID item)
    {
        if (item == InventoryItemID.Food)
            return SpritesStore[0];
        else if (item == InventoryItemID.Gun)
            return SpritesStore[1];

        return null;
    }

    private void PickUpItem(InventoryChangeEventArgs item)
    {
        ActualItemsImage.sprite = chooseSprite (item.newItemType);
        ItemsDescription.text = item.newItemType.ToString ();
    }

    private void DropItem()
    {
        ActualItemsImage.sprite = null;
        ItemsDescription.text = " ";
    }

    public void Drop()
    {
        Inventory.Drop ();
    }
}
