using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformerUIController : MonoBehaviour
{
    public GameObject Transformer;

    /// <summary>
    /// Contain all the images available using the type of the item
    /// </summary>
    private Dictionary<InventoryItemID, Sprite> spritesStorage;

    void Awake()
    {
        var imagesStorage = GameObject.FindGameObjectWithTag("MasterUI").GetComponent<UIMaster>().InventoryUI.ImagesStorage;

        spritesStorage = new Dictionary<InventoryItemID, Sprite>();

        foreach (InventoryUIController.ItemImage image in imagesStorage)
        {
            spritesStorage.Add(image.type, image.sprite);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenUIDialog()
    {

    }

    public void CloseUIDialog()
    {

    }

    public void UpdateState()
    {

    }
}
