using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumeItemsUI : BuildableObjectUI
{
    /// <summary>
    /// The consume items stage component.
    /// </summary>
    private ConsumeItemsStage consumeItemsStageComponent;

    /// <summary>
    /// Background for items images, that changes dynamically
    /// </summary>
    private RectTransform BackgroundPanel;

    /// <summary>
    /// Contain all the images available using the type of the item
    /// </summary>
    private Dictionary<InventoryItemID, Sprite> spritesStorage = new Dictionary<InventoryItemID, Sprite>();

    /// <summary>
    /// The required items dictionary, it's a pair of item type(Food,Burger, etc.) and count if the needed items.
    /// </summary>
    private Dictionary<InventoryItemID, int> requiredItemsDictionary = new Dictionary<InventoryItemID, int>();

    /// <summary>
    /// List of all images, that appeared in the UI
    /// </summary>
    private List<Image> NeededItemsImages = new List<Image>();

    /// <summary>
    /// Offset between images
    /// </summary>
    private float offsetImagePosition = -0.85f;
    /// <summary>
    /// Offset for changing background
    /// </summary>
    private float offsetBackground = 90f;

    public ConsumeItemsUI(GameObject buildableObject, Text state, Canvas canvasUI, RectTransform backgroundImagesPanel) : 
        base(buildableObject, state, canvasUI)
    {
        consumeItemsStageComponent = buildableGameObject.GetComponentInChildren<ConsumeItemsStage>();
        BackgroundPanel = backgroundImagesPanel;

        foreach (var image in GameObject.FindGameObjectWithTag("MasterUI").GetComponent<UIMaster>().InventoryUI.ImagesStorage)
        {
            spritesStorage.Add(image.type, image.sprite);
        }

        // Consume items events
        consumeItemsStageComponent.OnDictionaryLoaded += OnDictionaryLoaded;
        consumeItemsStageComponent.OnItemAccepted += OnItemAccepted;
    }

    /// <summary>
    /// Updates items images in the UI
    /// </summary>
    public void UpdateNeededItemsImages()
    {
        foreach (var item in NeededItemsImages)
        {
            GameObject.Destroy(item.gameObject);
            Debug.Log("destroy" + item.name);
        }
        NeededItemsImages.Clear();
        CreateNewNeededItemsImages();
    }

    /// <summary>
    /// Creates new images in the UI
    /// </summary>
    private void CreateNewNeededItemsImages()
    {
        Image tmpImage;
        Vector3 position = transform.position;
        int index = 0;

        // Create prefab Image for each item's image
        GameObject prefabObject = new GameObject();
        Image imagePrefab = prefabObject.AddComponent<Image>();

        // Create one image for each item
        foreach (var item in requiredItemsDictionary)
        {
            // If count of needed items is greater than zero
            if (item.Value > 0)
            {
                // Get sprite
                imagePrefab.sprite = spritesStorage[item.Key];
                // Update position
                position += new Vector3(index * offsetImagePosition, 0, 0);
                // Change Background panel size
                BackgroundPanel.offsetMax += new Vector2(index * offsetBackground, 0);
                // Create new image for UI (parent will be this object)
                tmpImage = GameObject.Instantiate<Image>(imagePrefab, position, transform.rotation, transform);
                // Change name to item's name
                tmpImage.name = item.Key.ToString();

                ///TODO     Amount of each items, create text for each image

                NeededItemsImages.Add(tmpImage);
                index++;
            }
        }
        GameObject.Destroy(prefabObject);
    }

    /// <summary>
    /// Gets required items from the stage
    /// </summary>
    private void getRequiredItems()
    {
        // Gets the dictionary
        requiredItemsDictionary = consumeItemsStageComponent.RequiredItemsInDictionary;
    }

    /// <summary>
    /// Consumes the items stage.
    /// </summary>
    /// <param name="source">Source.</param>
    /// <param name="consumeItemsStageArgs">Consume items stage arguments.</param>
    public void OnDictionaryLoaded(BuildStage source, ConsumeItemsStageArgs consumeItemsStageArgs)
    {
        getRequiredItems();
        UpdateNeededItemsImages();
        UpdateState("Needed");
    }

    /// <summary>
    /// Ons the item accepted.
    /// </summary>
    /// <param name="source">Source.</param>
    /// <param name="consumeItemsStageArgs">Consume items stage arguments.</param>
    public void OnItemAccepted(BuildStage source, ConsumeItemsStageArgs consumeItemsStageArgs)
    {
        getRequiredItems();
        UpdateNeededItemsImages();
        UpdateState("Needed");
    }
}
