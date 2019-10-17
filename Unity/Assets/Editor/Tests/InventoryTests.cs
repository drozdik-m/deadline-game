using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class InventoryTests
    {
        [Test]
        public void CanDropFromInventory()
        {
            // arrange
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<InventoryItem>();
            gameObject.AddComponent<Inventory>();
            Inventory inventory = gameObject.GetComponent<Inventory>();
            inventory.CurrentItem = gameObject.GetComponent<InventoryItem>();
            Assume.That(inventory.CurrentItem != null);

            // act
            Assume.That(inventory.Drop());

            // assert
            Assert.That(inventory.CurrentItem == null);
        }

        [Test]
        public void CanPickupToInventory()
        {
            // arrange
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<InventoryItem>();
            gameObject.AddComponent<Inventory>();
            Inventory inventory = gameObject.GetComponent<Inventory>();
            InventoryItem inventoryItem = gameObject.GetComponent<InventoryItem>();

            // act
            Assume.That(inventory.PickUp(inventoryItem));

            // assert
            Assert.That(inventory.CurrentItem == inventoryItem);
        }

        [Test]
        public void CantPickupMoreItems()
        {
            GameObject inventoryGO = new GameObject();
            inventoryGO.AddComponent<Inventory>();
            Inventory inventory = inventoryGO.GetComponent<Inventory>();

            GameObject invItem1GO = new GameObject();
            invItem1GO.AddComponent<InventoryItem>();

            GameObject invItem2GO = new GameObject();
            invItem2GO.AddComponent<InventoryItem>();

            Assume.That(inventory.PickUp(invItem1GO.GetComponent<InventoryItem>()));
            Assert.That(!inventory.PickUp(invItem2GO.GetComponent<InventoryItem>()));
        }
    }
}
