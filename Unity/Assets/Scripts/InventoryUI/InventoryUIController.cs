using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{

    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory.OnChange += PickUp;
    }

    public void PickUp(Inventory caller, InventoryChangeEventArgs e)
    {
        Debug.Log ("Pick up " + e.newItemType.ToString() );
    }

    public void Drop(Inventory caller, InventoryChangeEventArgs e)
    {
        Debug.Log ("Drop event action");
    }

}
