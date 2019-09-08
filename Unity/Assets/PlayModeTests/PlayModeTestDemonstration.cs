using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayModeTestDemonstration
    {
        [UnityTest]
        public IEnumerator InteractableClickToPickUpInventoryItem()
        {
            // setup scene
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("HouseScenery"));

            // find test gameobjects
            GameObject player = GameObject.FindWithTag("Player");
            GameObject bookInteractable = GameObject.Find("HouseScenery(Clone)/----------WORLD OBJECTS----------/Livingroom/BookInteractable/BookModel/Interactable");
            GameObject mainInventory = GameObject.FindWithTag("MainInventory");
            Assume.That(player != null);
            Assume.That(bookInteractable != null);
            Assume.That(mainInventory != null);

            // get test components
            PlayerMovementController playerMovementController = player.GetComponent<PlayerMovementController>();
            Interactable bookInteractableComp = bookInteractable.GetComponent<Interactable>();
            Inventory mainInventoryComp = mainInventory.GetComponent<Inventory>();
            Assume.That(playerMovementController != null);
            Assume.That(bookInteractableComp != null);
            Assume.That(mainInventoryComp != null);

            // check if the inventory is empty
            Assume.That(mainInventoryComp.CurrentItem == null);

            // call start method for initializing NavMesh agent
            playerMovementController.GetType().GetTypeInfo().GetDeclaredMethod("Start").Invoke(playerMovementController, null);

            // simulate user click
            playerMovementController.OnInteractableClick(bookInteractableComp);

            // wait for some time, so the player has time to get the item
            yield return new WaitForSeconds(4);

            // assert that inventory is not empty anymore
            Assert.That(mainInventoryComp.CurrentItem != null);
        }
    }
}
