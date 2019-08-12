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

    private float offsetImagePosition = -0.85f;
    private float offsetBackground = 90f;

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

        transform.LookAt(Camera.main.transform.position);
    }

    public void OpenUIDialog()
    {
        gameObject.SetActive(true);
    }

    public void CloseUIDialog()
    {
        gameObject.SetActive(false);
    }

    public void UpdateState()
    {
        string stateText;
        if (requiredItemsDictionary.Count > 0)
            stateText = "Prepared";
        else
            stateText = "Completed";

        StateText.text = stateText;
    }

    private void CreateNewNeededItemsImages()
    {
        Image tmpImage;
        Vector3 position = transform.position;
        int index = 0;

        foreach (var item in requiredItemsDictionary)
        {
            if (item.Value > 0)
            {
                Debug.Log(item.Key.ToString() + " " + item.Value.ToString() );
                ImagePrefabItem.sprite = spritesStorage[item.Key];
                position += new Vector3(index * offsetImagePosition, 0, 0);
                BackgroundPanel.offsetMax += new Vector2(index * offsetBackground, 0);
                tmpImage = GameObject.Instantiate<Image>(ImagePrefabItem, position, transform.rotation, transform);
                tmpImage.name = item.Key.ToString();
                NeededItemsImages.Add(tmpImage);
                index ++;
            }
        }
    }

    public void UpdateNeededItemsImages()
    {
        foreach (var item in NeededItemsImages)
        {
            GameObject.Destroy(item.gameObject);
            Debug.Log("destroy" + item.name);
        }
        NeededItemsImages.Clear();
        CreateNewNeededItemsImages();
        UpdateState();
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
        UpdateNeededItemsImages();
    }
    /// <summary>
    /// Ons the item accepted.
    /// </summary>
    /// <param name="source">Source.</param>
    /// <param name="consumeItemsStageArgs">Consume items stage arguments.</param>
    public void OnItemAccepted(BuildStage source, ConsumeItemsStageArgs consumeItemsStageArgs) {
        getRequiredItems();
        UpdateNeededItemsImages();
    }
}
