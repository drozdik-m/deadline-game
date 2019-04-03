using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{

    public Inventory inventory;

    private bool isInventoryEmpty;

    // Start is called before the first frame update
    void Start()
    {
        inventory.OnChange += OnChange;
    }

    public void OnChange( Inventory caller, InventoryChangeEventArgs e)
    {
        Debug.Log ("On change " + e.newItemType.ToString() );
    }
}
