using System;
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
    /// Item prefab, which will be provided to the player, into his invetory
    /// </summary>
    public GameObject ProvidedItemPrefab;
    /// <summary>
    /// If true, infinite providing is enabled
    /// </summary>
    public bool InfiniteProviding;
    /// <summary>
    ///  Number of items to provide
    /// </summary>
    public int NumberOfItems;
    /// <summary>
    /// Provider items depleeted handler.
    /// </summary>
    public delegate void ProviderItemsDepleetedHandler(ItemProvider source, ItemProviderArgs providedItem);
    /// <summary>
    /// Occurs when provided items are depleeted.
    /// </summary>
    public event ProviderItemsDepleetedHandler ProvidedItemsDepleeted;


    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
             ProvideItem();
        }
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

            if(NumberOfItems == 0 && !InfiniteProviding)
            {
                OnProviderItemsDepleeted();
                return;
            }

            if (!InfiniteProviding)
                NumberOfItems--;

            var instance = Instantiate(ProvidedItemPrefab);

            var invetoryComponent = instance.GetComponent<InventoryItem>();

            playerInvetory.PickUp(invetoryComponent);

        }


    }

    protected virtual void OnProviderItemsDepleeted()
    {
        var prefabInvetoryComponent = ProvidedItemPrefab.GetComponent<InventoryItem>();

        if (ProvidedItemsDepleeted != null)
            ProvidedItemsDepleeted(this, new ItemProviderArgs(prefabInvetoryComponent.ItemType));
    }


}
