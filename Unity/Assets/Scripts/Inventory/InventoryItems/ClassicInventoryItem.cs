using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicInventoryItem : InventoryItem
{
    public GameObject OptionalDropTarget;

    private void Awake()
    {
        PickedUp += PickUpFromScene;
        Dropped += DropIntoScene;
    }

    private void PickUpFromScene(object sender, InventoryItemEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void DropIntoScene(object sender, InventoryItemEventArgs e)
    {
        throw new NotImplementedException();
    }
}
