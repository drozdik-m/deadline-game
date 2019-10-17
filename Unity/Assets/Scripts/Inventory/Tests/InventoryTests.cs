using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTests : MonoBehaviour
{
    public InventoryInspectorHelper InspectorHelper;

    public void PickUpObject1()
    {
        InspectorHelper.PickUpThis();
    }

    public void DropObject1()
    {
        InspectorHelper.DropThis();
    }

}
