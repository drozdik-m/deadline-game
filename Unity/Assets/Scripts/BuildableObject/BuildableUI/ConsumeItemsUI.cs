using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class represents UI for consume items stage
/// </summary>
public class ConsumeItemsUI : BuildableObjectUI
{
    /// <summary>
    /// Consume items stage component.
    /// </summary>
    private ConsumeItemsStage consumeItemsStageComponent;

    /// <summary>
    /// Background for items images, that changes dynamically
    /// </summary>
    public RectTransform backgroundPanel;

    /// <summary>
    /// Prefab image for items counter
    /// </summary>
    public Text textPrefabCounterItems;

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
    private List<Image> neededItemsImages = new List<Image>();

    /// <summary>
    /// Offset between images
    /// </summary>
    private float offsetImagePosition = 1f;

    /// <summary>
    /// Offset between text counter
    /// </summary>
    private float offsetTextCounterPosition = -0.45f;

    /// <summary>
    /// Offset for changing background
    /// </summary>
    private float offsetBackground = 95f;

    /// <summary>
    /// Sets necessary values for stage UI
    /// </summary>
    /// <param name="buildableObject">Game Object that contains Buildable object</param>
    /// <param name="state">State text UI</param>
    /// <param name="backgroundImagesPanel">Background for Items images</param>
    /// <param name="prefabTextCounter">Prefab of the text for counter of desired Items</param>
    public void SetUI(GameObject buildableObject, Text state, RectTransform backgroundImagesPanel, Text prefabTextCounter) 
    {
        // Set all required items with base
        base.SetUI(buildableObject, state);

        consumeItemsStageComponent = buildableGameObject.GetComponentInChildren<ConsumeItemsStage>();
        backgroundPanel = backgroundImagesPanel;
        textPrefabCounterItems = prefabTextCounter;

        foreach (var image in GameObject.FindGameObjectWithTag("MasterUI").GetComponent<UIMaster>().InventoryUI.ImagesStorage)
        {
            spritesStorage.Add(image.type, image.sprite);
        }

        // Consume items events
        consumeItemsStageComponent.OnDictionaryLoaded += OnDictionaryLoaded;
        consumeItemsStageComponent.OnItemAccepted += OnItemAccepted;
    }

    /// <summary>
    /// Activates stage UI
    /// </summary>
    public override void Activate()
    {
        backgroundPanel.gameObject.SetActive(true);
        UpdateNeededItemsImages();
    }

    /// <summary>
    /// Deactivates stage UI
    /// </summary>
    public override void Deactivate()
    {
        backgroundPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Updates items images in the UI
    /// </summary>
    public void UpdateNeededItemsImages()
    {
        // Delete all items images and set background panel size to zero
        foreach (var item in neededItemsImages)
        {
            GameObject.Destroy(item.gameObject);
            backgroundPanel.offsetMax = Vector2.zero;
        }
        neededItemsImages.Clear();

        CreateNewNeededItemsImages();
    }

    /// <summary>
    /// Creates new images in the UI
    /// </summary>
    private void CreateNewNeededItemsImages()
    {
        Image tmpImage;
        Vector3 position;
        int index = 0;

        // Save rotation and set to zero
        Quaternion savedRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(Vector3.zero);

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
                position = transform.position + new Vector3(index * offsetImagePosition, 0, 0);
                // Change Background panel size
                backgroundPanel.offsetMax = new Vector2(index * offsetBackground, 0);
                // Create new image for UI (parent will be this object)
                tmpImage = GameObject.Instantiate<Image>(imagePrefab, position, transform.rotation, transform);
                // Change name to item's name
                tmpImage.name = item.Key.ToString();

                neededItemsImages.Add(tmpImage);
                index++;

                // Change rotation of the text
                Vector3 textRotation = new Vector3(0, 180, 0);
                // Set amount of items text
                textPrefabCounterItems.text = item.Value.ToString();
                // Create new items counter (parent will be image of the item)
                GameObject.Instantiate<Text>(textPrefabCounterItems, position + new Vector3(offsetTextCounterPosition, 0.3f, 0), Quaternion.Euler(textRotation), tmpImage.transform);


            }
        }
        GameObject.Destroy(prefabObject);
        transform.rotation = savedRotation;
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
        UpdateStateText("Needed");
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
        UpdateStateText("Needed");
    }
}
