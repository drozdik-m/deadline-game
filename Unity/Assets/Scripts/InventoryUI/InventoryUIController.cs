using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUIController : MonoBehaviour
{

    public Inventory Inventory;

    public Sprite[] ItemsSpritesStore;

    public Image ActualImageItem;

    private bool isInventoryEmpty;

    // Start is called before the first frame update
    void Start()
    {
        Inventory.OnChange += OnChange;
        isInventoryEmpty = true;
    }

    public void OnChange(Inventory caller, InventoryChangeEventArgs e)
    {
        Debug.Log ("On change " + e.newItemType.ToString() );
    }

    private void chooseSprite(InventoryItemID item)
    {
        if (item == InventoryItemID.Food)
            ActualImageItem.sprite = ItemsSpritesStore[0];
        else if (item == InventoryItemID.Gun)
            ActualImageItem.sprite = ItemsSpritesStore[1];
    }
}
