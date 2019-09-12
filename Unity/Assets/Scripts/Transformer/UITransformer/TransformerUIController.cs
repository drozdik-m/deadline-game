using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class controlls UI of transformer items. Shows all needed items in images and state of the transformation.
/// </summary>
public class TransformerUIController : MonoBehaviour
{
    /// <summary>
    /// Text item's state (Needed, Preparing, Completed)
    /// </summary>
    public Text StateText;
    /// <summary>
    /// Background for items images, that changes dynamically
    /// </summary>
    public RectTransform BackgroundPanel;
    /// <summary>
    /// Prefab image for items images
    /// </summary>
    public Image ImagePrefabItem;

    /// <summary>
    /// Prefab image for items images
    /// </summary>
    public Text TextPrefabCounterItems;

    /// <summary>
    /// Canvas of the Transformer UI object
    /// </summary>
    private Canvas transformerUICanvas;

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
    /// <summary>
    /// Wait and give item stage component
    /// </summary>
    private WaitAndGive waitAndGiveComponent;

    /// <summary>
    /// Minimum disstance to the player to appear
    /// </summary>
    public float MinimumDistanceToAppear = 3;
    /// <summary>
    /// Offset between images
    /// </summary>
    private float offsetImagePosition = 1.1f;
    /// <summary>
    /// Offset between text counter
    /// </summary>
    private float offsetTextCounterPosition = -0.45f;
    /// <summary>
    /// Offset for changing background
    /// </summary>
    private float offsetBackground = 90f;
    /// <summary>
    /// Checks if items is completed
    /// </summary>
    private bool isCompleted = false;

    /// <summary>
    /// List of all images, that appeared in the UI
    /// </summary>
    private List<Image> NeededItemsImages = new List<Image>();

    void Awake()
    {
        spritesStorage = new Dictionary<InventoryItemID, Sprite>();

        foreach (var image in GameObject.FindGameObjectWithTag("MasterUI").GetComponent<UIMaster>().InventoryUI.ImagesStorage)
        {
            spritesStorage.Add(image.type, image.sprite);
        }
    }

    void Start()
    {
        CreateNewNeededItemsImages();

        transformerUICanvas = GetComponent<Canvas>();

        // Gets the ConsumeItemsStage component
        consumeItemsStageComponent = BuildableGameObject.GetComponentInChildren<ConsumeItemsStage>();
        if (!consumeItemsStageComponent)
        {
            Debug.LogError("ConsumeItemsStage component not found!");
            return;
        }

        waitAndGiveComponent = BuildableGameObject.GetComponentInChildren<WaitAndGive>();
        if (!waitAndGiveComponent)
        {
            Debug.LogError("WaitAndGive component not found!");
            return;
        }

        // Consume items events
        consumeItemsStageComponent.OnDictionaryLoaded += OnDictionaryLoaded;
        consumeItemsStageComponent.OnItemAccepted += OnItemAccepted;

        // WaitAndGive events
        waitAndGiveComponent.OnTransformationFinished += OnTransformationFinished;
        waitAndGiveComponent.OnTransformationStarted += OnTransformationStarted;
    }

    private void Update()
    {
        if (CheckCloseToTag())
        {
            OpenUIDialog();
        }
        else
        {
            CloseUIDialog();
        }
    }

    /// <summary>
    /// Opens Transformer UI
    /// </summary>
    public void OpenUIDialog()
    {
        if (!isCompleted)
            transformerUICanvas.enabled = true;
    }

    /// <summary>
    /// Closes Transformer UI
    /// </summary>
    public void CloseUIDialog()
    {
        transformerUICanvas.enabled = false;
    }

    /// <summary>
    /// Changes text state
    /// </summary>
    /// <param name="stateText">Text of the state(Needed, Preparing, Completed)</param>
    public void UpdateState(string stateText)
    {
        StateText.text = stateText;
    }

    /// <summary>
    /// Creates new images in the UI
    /// </summary>
    private void CreateNewNeededItemsImages()
    {
        Image tmpImage;
        Vector3 position;
        int index = 0;

        Quaternion originalRotation = transform.rotation;

        transform.rotation = Quaternion.Euler(Vector3.zero);

        // Create one image for each item
        foreach (var item in requiredItemsDictionary)
        {
            // If count of needed items is greater than zero
            if (item.Value > 0)
            {
                // Get sprite
                ImagePrefabItem.sprite = spritesStorage[item.Key];
                // Update position
                position = transform.position + new Vector3(index * offsetImagePosition, 0, 0);
                // Change Background panel size
                BackgroundPanel.offsetMax = new Vector2(index * offsetBackground, 0);
                // Create new image for UI (parent will be this object)
                tmpImage = GameObject.Instantiate<Image>(ImagePrefabItem, position, transform.rotation, transform);
                // Change name to item's name
                tmpImage.name = item.Key.ToString();

                NeededItemsImages.Add(tmpImage);
                index++;

                TextPrefabCounterItems.text = item.Value.ToString();
                Vector3 rot = new Vector3(0, 180, 0);
                GameObject.Instantiate<Text>(TextPrefabCounterItems, position + new Vector3(offsetTextCounterPosition, 0.3f, 0), Quaternion.Euler(rot), tmpImage.transform);

            }
        }
        transform.rotation = originalRotation;
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
            BackgroundPanel.offsetMax = Vector2.zero;
        }
        NeededItemsImages.Clear();
        CreateNewNeededItemsImages();
    }

    /// <summary>
    /// Gets required items from the stage
    /// </summary>
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

    /// <summary>
    /// Transformation of the item is finished
    /// </summary>
    /// <param name="source">Source.</param>
    /// <param name="consumeItemsStageArgs">Wait and give stage arguments.</param>
    public void OnTransformationFinished(BuildStage source, WaitAndGiveArgs consumeItemsStageArgs)
    {
        // When transformation finished
        UpdateState("Completed");
        isCompleted = true;
    }

    /// <summary>
    /// Transformation of the item is started
    /// </summary>
    /// <param name="source">Source.</param>
    /// <param name="consumeItemsStageArgs">Wait and give stage arguments.</param>
    public void OnTransformationStarted(BuildStage source, WaitAndGiveArgs consumeItemsStageArgs)
    {
        // When transformation starts
        UpdateState("Preparing");
        BackgroundPanel.gameObject.SetActive(false);
    }

    bool CheckCloseToTag()
    {
        GameObject goWithTag = GameObject.FindGameObjectWithTag("Player");

        if (Vector3.Distance(transform.position, goWithTag.transform.position) <= MinimumDistanceToAppear)
                return true;
        return false;
    }
}
