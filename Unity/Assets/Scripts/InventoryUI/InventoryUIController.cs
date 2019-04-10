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
    /// Image of the item in the UI inventory
    /// </summary>
    public Image ActualItemsImage;

    /// <summary>
    /// Contains type of the item in the inventory
    /// </summary>
    public Text ItemTypeText;

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

    /// <summary>
    /// Contain all the images available using the type of the item
    /// </summary>
    private Dictionary<InventoryItemID, Sprite> spritesStorage;

    // Start is called before the first frame update
    void Start()
    {
        if (Inventory == null)
            Inventory = GameObject.FindGameObjectWithTag ("MainInventory")?.GetComponent<Inventory> ();

        if(Inventory != null)
            Inventory.OnChange += OnChange;

        ActualItemsImage.sprite = null;
        ItemTypeText.text = "";
        isInventoryEmpty = true;

        spritesStorage = new Dictionary<InventoryItemID, Sprite> ();

        foreach (ItemImage image in ImagesStorage)
        {
            spritesStorage.Add (image.type, image.sprite);
        }
    }

    /// <summary>
    /// Drop function for button
    /// </summary>
    public void Drop()
    {
        if (Inventory != null)
            Inventory.Drop ();
    }

    /// <summary>
    /// Change UI inventory status, if there is an item in the
    /// inventory or not
    /// </summary>
    /// <param name="inv">Inventory from where the method was called</param>
    /// <param name="item">New item in the inventory</param>
    public void OnChange(Inventory inv, InventoryChangeEventArgs item)
    {
        if (item.newItemType == InventoryItemID.None)
        {
            ChangeDropItemUI ();
            isInventoryEmpty = true;
        } else if (isInventoryEmpty)
        {
            ChangePickUpItemUI (item);
            isInventoryEmpty = false;
        }
    }

    /// <summary>
    /// Set image and type of the new item in the UI inventory
    /// </summary>
    /// <param name="item">New item in the inventory</param>
    private void ChangePickUpItemUI(InventoryChangeEventArgs item)
    {
        ActualItemsImage.sprite = spritesStorage[item.newItemType];
        ItemTypeText.text = item.newItemType.ToString ();
    }

    /// <summary>
    /// Remove image and type of the drop item 
    /// </summary>
    private void ChangeDropItemUI()
    {
        ActualItemsImage.sprite = null;
        ItemTypeText.text = " ";
    }
}
