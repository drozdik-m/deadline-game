using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{

    public Inventory Inventory;

    public Image ActualImageItem;

    public Text ItemsDescription;

    public Sprite[] SpritesStore;

    private bool isInventoryEmpty;

    // Start is called before the first frame update
    void Start()
    {
        Inventory.OnChange += OnChange;
        ActualImageItem.sprite = null;
        ItemsDescription.text = "";
        isInventoryEmpty = true;
    }

    public void OnChange(Inventory caller, InventoryChangeEventArgs item)
    {
        if (isInventoryEmpty)
        {
            PickUp (item);
            isInventoryEmpty = false;
        } else
        {
            Drop (item);
            isInventoryEmpty = true;
        }

    }

    private Sprite chooseSprite(InventoryItemID item)
    {
        if (item == InventoryItemID.Food)
            return SpritesStore[0];
        else if (item == InventoryItemID.Gun)
            return SpritesStore[1];

        return null;
    }

    private void PickUp(InventoryChangeEventArgs item)
    {
        ActualImageItem.sprite = chooseSprite (item.newItemType);
        ItemsDescription.text = item.newItemType.ToString ();

    }

    private void Drop(InventoryChangeEventArgs item)
    {
        ActualImageItem.sprite = null;
        ItemsDescription.text = "";
    }
}
