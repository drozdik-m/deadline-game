using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Class controls elements of UI inventory
/// </summary>
[System.Serializable]
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
    /// Struct that contain image and type of the item
    /// </summary>
    [System.Serializable]
    public struct ItemImage
    {
        public InventoryItemID type;
        public Sprite sprite;
    }

    /// <summary>
    /// Stores all images for items
    /// </summary>
    public ItemImage[] ImagesStorage;

    /// <summary>
    /// Status of inventory
    /// </summary>
    private bool isInventoryEmpty;

    private Dictionary<InventoryItemID, Sprite> spritesStorage;

    // Start is called before the first frame update
    void Start()
    {
        Inventory.OnChange += OnChange;
        ActualItemsImage.sprite = null;
        ItemsDescription.text = "";
        isInventoryEmpty = true;

        spritesStorage = new Dictionary<InventoryItemID, Sprite> ();

        foreach (ItemImage image in ImagesStorage)
        {
            spritesStorage.Add (image.type, image.sprite);
        }
    }

    /// <summary>
    /// Drop function fo button
    /// </summary>
    public void Drop()
    {
        Inventory.Drop ();
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

    private void PickUpItem(InventoryChangeEventArgs item)
    {
        ActualItemsImage.sprite = spritesStorage[item.newItemType];
        ItemsDescription.text = item.newItemType.ToString ();
    }

    private void DropItem()
    {
        ActualItemsImage.sprite = null;
        ItemsDescription.text = " ";
    }
}
