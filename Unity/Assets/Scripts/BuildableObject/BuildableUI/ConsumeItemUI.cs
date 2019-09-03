using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumeItemUI : BuildableObjectUI
{
    /// <summary>
    /// Background for items images, that changes dynamically
    /// </summary>
    private RectTransform backgroundPanel;

    private ConsumeItemStage consumeItemStageComponent;

    private Sprite desiredItemSprite;

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

    public override void Activate()
    {
        backgroundPanel.gameObject.SetActive(true);
        UpdateStateText("Needed");
        CreateNewDesiredItem();
    }

    public override void Deactivate()
    {
        backgroundPanel.gameObject.SetActive(false);
    }

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
