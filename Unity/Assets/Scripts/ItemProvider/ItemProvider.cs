using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProvider : MonoBehaviour
{
    /// <summary>
    /// Refference to the player
    /// </summary>
    private Inventory playerInvetory;
    /// <summary>
    /// Item, which will be provided to the player, into his invetory
    /// </summary>
    public InventoryItemID ProvidedItemType;

    public bool InfinitProviding;
    public int NumberOfItems;

    private void Start()
    {
       // ProvideItem();
    }


    public void ProvideItem()
    {
        GameObject invetoryGameObject = GameObject.FindGameObjectWithTag("MainInventory");

        if (invetoryGameObject)
        {
            playerInvetory = invetoryGameObject.GetComponent<Inventory>();
            if (!playerInvetory)
            {
                Debug.Log("ItemProvider: Invetory script not found!");
            }
        }
        else
        {
            Debug.Log("ItemProvider: Invetory gameobject not found!");
        }

        if (!playerInvetory.CurrentItem)
        {


            GameObject providedItemGameObject = new GameObject();

            InventoryItem it = providedItemGameObject.AddComponent(typeof(InventoryItem)) as InventoryItem;


            it.ItemType = ProvidedItemType;

            var instance = Instantiate(providedItemGameObject);

            playerInvetory.PickUp(it);
 


  
        }


    }


}
