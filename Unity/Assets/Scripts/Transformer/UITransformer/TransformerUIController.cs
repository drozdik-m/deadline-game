using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformerUIController : MonoBehaviour
{
    public BuildableObject TransformerItem;
    public Text StateText;
    public RectTransform BackgroundPanel;
    public Image ImagePrefabItem;

    /// <summary>
    /// Contain all the images available using the type of the item
    /// </summary>
    private Dictionary<InventoryItemID, Sprite> spritesStorage;
    /// <summary>
    /// The buildable game object refference.
    /// </summary>
    public GameObject BuildableGameObject;
    /// <summary>
    /// The required items dictionary, it's a pair of item type(Food,Burger, etc.) and count if the needed items.
    /// </summary>
    private Dictionary<InventoryItemID, int> requiredItemsDictionary = new Dictionary<InventoryItemID, int>();
    /// <summary>
    /// The consume items stage component.
    /// </summary>
    private ConsumeItemsStage consumeItemsStageComponent;

    private List<Image> NeededItemsImages = new List<Image>();

    void Awake()
    {
        var imagesStorage = GameObject.FindGameObjectWithTag("MasterUI").GetComponent<UIMaster>().InventoryUI.ImagesStorage;

        spritesStorage = new Dictionary<InventoryItemID, Sprite>();

        foreach (var image in imagesStorage)
        {
            spritesStorage.Add(image.type, image.sprite);
        }


    }

    void Start()
    {
        CreateNewNeededItemsImages();

        // Gets the ConsumeItemsStage component
        consumeItemsStageComponent = BuildableGameObject.GetComponentInChildren<ConsumeItemsStage>();
        if (!consumeItemsStageComponent)
        {
            Debug.Log("ConsumeItemsStage component not found!");
            return;
        }
        consumeItemsStageComponent.OnDictionaryLoaded += OnDictionaryLoaded;
        consumeItemsStageComponent.OnItemAccepted += OnItemAccepted;
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

    private void CreateNewNeededItemsImages()
    {
        Image tmpImage;
        Vector3 position = transform.position;
        float offset = -1.3f;
        float offsetBackground = 90f;

        ImagePrefabItem.sprite = spritesStorage[InventoryItemID.Axe];
        tmpImage = GameObject.Instantiate<Image>(ImagePrefabItem, position, transform.rotation, transform);
        tmpImage.name = InventoryItemID.Axe.ToString();
        NeededItemsImages.Add(tmpImage);

        ImagePrefabItem.sprite = spritesStorage[InventoryItemID.Book];
        position += new Vector3(offset, 0, 0);
        BackgroundPanel.offsetMax += new Vector2(offsetBackground, 0);
        tmpImage =  GameObject.Instantiate<Image>(ImagePrefabItem, position, transform.rotation, transform);
        tmpImage.name = InventoryItemID.Book.ToString();
        NeededItemsImages.Add(tmpImage);

        ImagePrefabItem.sprite = spritesStorage[InventoryItemID.Broom];
        position += new Vector3(offset, 0, 0);
        BackgroundPanel.offsetMax += new Vector2(offsetBackground, 0);
        GameObject.Instantiate<Image>(ImagePrefabItem, position, transform.rotation, transform);

        transform.LookAt(Camera.main.transform.position);

    }

    public void UpdateNeededItemsImages()
    {
        foreach (var item in NeededItemsImages)
        {
            GameObject.Destroy(item);
        }

        CreateNewNeededItemsImages();
    }


    public void getRequiredItems()
    {
     
        // Gets the dictionary
        requiredItemsDictionary = consumeItemsStageComponent.RequiredItemsInDictionary;
    }

    /// <summary>
    /// Consumes the items stage.
    /// </summary>
    /// <param name="source">Source.</param>
    /// <param name="consumeItemsStageArgs">Consume items stage arguments.</param>
    public void OnDictionaryLoaded(BuildStage source, ConsumeItemsStageArgs consumeItemsStageArgs) {
        getRequiredItems();
    }
    /// <summary>
    /// Ons the item accepted.
    /// </summary>
    /// <param name="source">Source.</param>
    /// <param name="consumeItemsStageArgs">Consume items stage arguments.</param>
    public void OnItemAccepted(BuildStage source, ConsumeItemsStageArgs consumeItemsStageArgs) {
        getRequiredItems();
    }
}
