using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformerUIController : MonoBehaviour
{
    public GameObject TransformerItem;
    public Text StateText;

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
        Image imageItem = new GameObject().AddComponent<Image>();
        Vector3 position = transform.position;

        imageItem.sprite = spritesStorage[InventoryItemID.Axe];
        GameObject.Instantiate<Image>(imageItem, position, transform.rotation, transform);

        imageItem.sprite = spritesStorage[InventoryItemID.Book];
        position += new Vector3(30, 0, 0);
        GameObject.Instantiate<Image>(imageItem, position, transform.rotation, transform);
    }

    public void OpenUIDialog()
    {
        gameObject.SetActive(true);
    }

    public void CloseUIDialog()
    {
        gameObject.SetActive(false);
    }

    public void UpdateState(string stateText)
    {
        StateText.text = stateText;
    }
}
