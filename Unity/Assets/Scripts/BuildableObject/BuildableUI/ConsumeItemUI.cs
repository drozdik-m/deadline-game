using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class represents UI for consume item stage
/// </summary>
public class ConsumeItemUI : BuildableObjectUI
{
    /// <summary>
    /// Background for items images, that changes dynamically
    /// </summary>
    private RectTransform backgroundPanel;

    /// <summary>
    /// Consume Item stage component
    /// </summary>
    private ConsumeItemStage consumeItemStageComponent;

    /// <summary>
    /// Sprite of the desired item
    /// </summary>
    private Sprite desiredItemSprite;

    /// <summary>
    /// Sets necessary values for stage UI
    /// </summary>
    /// <param name="buildableObject">Game Object that contains Buildable object</param>
    /// <param name="state">State text UI</param>
    /// <param name="backgroundImagesPanel">Background for Items images</param>
    public void SetUI(GameObject buildableObject, Text state, RectTransform backgroundImagesPanel)
    {
        // Set all required items with base
        base.SetUI(buildableObject, state);

        consumeItemStageComponent = buildableGameObject.GetComponentInChildren<ConsumeItemStage>();
        backgroundPanel = backgroundImagesPanel;

        foreach (var image in GameObject.FindGameObjectWithTag("MasterUI").GetComponent<UIMaster>().InventoryUI.ImagesStorage)
        {
            if(consumeItemStageComponent.desiredItem == image.type)
            {
                desiredItemSprite = image.sprite;
            }
        }
    }

    /// <summary>
    /// Activates stage UI
    /// </summary>
    public override void Activate()
    {
        backgroundPanel.gameObject.SetActive(true);
        UpdateStateText("Need");
        CreateNewDesiredItem();
    }

    /// <summary>
    /// Deactivates stage UI
    /// </summary>
    public override void Deactivate()
    {
        backgroundPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Creates and sets new Image for desired Item
    /// </summary>
    private void CreateNewDesiredItem()
    {
        Image tmpImage;

        // Create prefab Image for item image
        GameObject prefabObject = new GameObject();
        Image imagePrefab = prefabObject.AddComponent<Image>();

        imagePrefab.sprite = desiredItemSprite;
        tmpImage = GameObject.Instantiate<Image>(imagePrefab, transform.position, transform.rotation, transform);
        tmpImage.name = consumeItemStageComponent.desiredItem.ToString();
    }
}
