using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic item implementation. 
/// </summary>
public class ClassicInventoryItem : InventoryItem
{
    public GameObject PickableObjectInScene = null;
    public GameObject OverrideDropTarget = null;
    

    private void Awake()
    {
        PickedUp += PickUpFromScene;
        Dropped += DropIntoScene;
    }

    /// <summary>
    /// Disabled the item in the scene.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    private void PickUpFromScene(object sender, InventoryItemEventArgs e)
    {
        PickableObjectInScene.SetActive(false);
    }

    /// <summary>
    /// If the "OptionalDropTarget" is not null, drop the target at "OptionalDropTarget" coordinates and rotation values.
    /// Else, the item is dropped at inventory coordinates and rotation.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    private void DropIntoScene(object sender, InventoryItemEventArgs e)
    {
        GameObject dropPoint = OverrideDropTarget != null ? OverrideDropTarget : e.RecommendedDropPoint;

        PickableObjectInScene.SetActive(true);

        PickableObjectInScene.transform.position = dropPoint.transform.position;
        PickableObjectInScene.transform.rotation = dropPoint.transform.rotation;
    }
}
