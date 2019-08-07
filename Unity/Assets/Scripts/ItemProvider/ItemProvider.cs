using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provider items depleeted handler.
/// </summary>
public delegate void ProviderItemsHandler(ItemProvider source, ItemProviderArgs providedItem);

/// <summary>
/// Item provider class, used to provide items to player's invetory
/// </summary>
public class ItemProvider : MonoBehaviour
{
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
    /// Occurs when provided items are depleeted
    /// </summary>
    public event ProviderItemsHandler OnProvidedItemsDepleeted;
    /// <summary>
    /// Occurs when on depleeted pickup try
    /// </summary>
    public event ProviderItemsHandler OnDepleetedProvideTry;
    /// <summary>
    /// Refference to the player
    /// </summary>
    public event ProviderItemsHandler OnItemProvided;
    /// <summary>
    /// The player's invetory component
    /// </summary>
    private Inventory playerInvetory;
    /// <summary>
    /// Prefab's invetory item component
    /// </summary>
    private InventoryItem prefabInvetoryComponent;
    /// <summary>
    /// The is depleeted.
    /// </summary>
    private bool isDepleeted;

    private void Start()
    {
        CheckPrefabValidity();

        prefabInvetoryComponent = ProvidedItemPrefab.GetComponent<InventoryItem>();

        isDepleeted = false;

        GameObject invetoryGameObject = GameObject.FindGameObjectWithTag("MainInventory");


        // Getting all necessary components
        if (invetoryGameObject)
        {
            playerInvetory = invetoryGameObject.GetComponent<Inventory>();
            if (!playerInvetory)
            {
                Debug.LogError("ItemProvider: Invetory script not found!");
            }
        }
        else
        {
            Debug.LogError("ItemProvider: Invetory gameobject not found!");
        }
    }
    /// <summary>
    /// Provides the specified item to the player's inventory
    /// </summary>
    public bool ProvideItem()
    {
       
        // Check the prefab validity
        if (!CheckPrefabValidity())
            return false;


        // Actual spawning and picking up
        if (!playerInvetory.CurrentItem)
        {
        
            if (NumberOfItems == 0 && !InfiniteProviding)
            {
                    DepleetedProvideTry();
                    return false;
            }

            var instance = Instantiate(ProvidedItemPrefab);

            var invetoryComponent = instance.GetComponent<InventoryItem>();

            bool pickupStatus = playerInvetory.PickUp(invetoryComponent);

            if (pickupStatus)
            {
                ItemProvided();
                if (!InfiniteProviding)
                    NumberOfItems--;
            }

            if(NumberOfItems == 0 && !InfiniteProviding)
            {
                isDepleeted = true;
                ProviderItemsDepleeted();
            }

            return pickupStatus;


        }
        return false;


    }
    /// <summary>
    /// Triggers, when items are depleeted
    /// </summary>
    protected virtual void ProviderItemsDepleeted()
    {
        OnProvidedItemsDepleeted?.Invoke(this, new ItemProviderArgs(prefabInvetoryComponent.ItemType,isDepleeted, NumberOfItems ));
    }
    /// <summary>
    /// Depleeteds the provide try.
    /// </summary>
    protected virtual void DepleetedProvideTry()
    {
        OnDepleetedProvideTry?.Invoke(this, new ItemProviderArgs(prefabInvetoryComponent.ItemType, isDepleeted, NumberOfItems));
    }
    /// <summary>
    /// Item was provided.
    /// </summary>
    protected virtual void ItemProvided()
    {
        OnItemProvided?.Invoke(this, new ItemProviderArgs(prefabInvetoryComponent.ItemType, isDepleeted, NumberOfItems));
    }
    /// <summary>
    /// Checks the prefab validity(presence of the InvetoryItem component).
    /// </summary>
    /// <returns><c>true</c>, if prefab validity was checked, <c>false</c> otherwise.</returns>
    private bool CheckPrefabValidity()
    {
        // Checking validity of the item prefab
        if (!ProvidedItemPrefab.GetComponent<InventoryItem>())
        {
            Debug.LogError("ItemProvider: The item prefab has not InvetoryItem component");
            return false;
        }
        return true;


    }

}
