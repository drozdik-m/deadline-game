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

    private void Start()
    {
        checkPrefabValidity();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
             ProvideItem();
        }
    }

    /// <summary>
    /// Provides the specified item to the player's inventory
    /// </summary>
    public void ProvideItem()
    {
        GameObject invetoryGameObject = GameObject.FindGameObjectWithTag("MainInventory");


        // Getting all necessary components
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

        // Check the prefab validity
        if (!checkPrefabValidity())
            return;


        // Actual spawning and picking up
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

    /// <summary>
    /// Checks the prefab validity(presence of the InvetoryItem component).
    /// </summary>
    /// <returns><c>true</c>, if prefab validity was checked, <c>false</c> otherwise.</returns>
    private bool checkPrefabValidity()
    {
        // Checking validity of the item prefab
        if (!ProvidedItemPrefab.GetComponent<InventoryItem>())
        {
            Debug.Log("ItemProvider: The item prefab has not InvetoryItem component");
            return false;
        }
        return true;


    }


}
