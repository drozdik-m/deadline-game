using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildableObjectUIController : MonoBehaviour
{
    /// <summary>
    /// The buildable game object refference.
    /// </summary>
    public GameObject BuildableGameObject;

    /// <summary>
    /// Background for items images, that changes dynamically
    /// </summary>
    public RectTransform BackgroundPanel;

    /// <summary>
    /// Text item's state (Needed, Preparing, Completed)
    /// </summary>
    public Text StateText;

    /// <summary>
    /// Prefab image for items counter
    /// </summary>
    public Text TextPrefabCounterItems;

    public Slider ProgressProcentSlider;

    /// <summary>
    /// Minimum disstance to the player to appear
    /// </summary>
    public float MinimumDistanceToAppear = 4;

    /// <summary>
    /// Canvas of the Transformer UI object
    /// </summary>
    private Canvas transformerUICanvas;

    /// <summary>
    /// Checks if items is completed
    /// </summary>
    private bool isCompleted = false;

    private BuildableObjectUI currentStageUI;

    private void Start()
    {
        transformerUICanvas = GetComponent<Canvas>();

        SetNewStage (BuildableGameObject.GetComponent<BuildStageCollection>().stages[0]);
        currentStageUI.Activate();

        BuildableGameObject.GetComponent<BuildableObject>().OnChange += OnChangeStage;
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
        if(!isCompleted)
            transformerUICanvas.enabled = true;
    }

    /// <summary>
    /// Closes Transformer UI
    /// </summary>
    public void CloseUIDialog()
    {
        transformerUICanvas.enabled = false;
    }

    private void SetNewStage(BuildStage buildStage)
    { 
        Type stageType = buildStage.GetType();
        Debug.Log("It is " + stageType.Name);

        if (stageType == typeof(ConsumeItemsStage))
        {
            ConsumeItemsUI stageUI = new ConsumeItemsUI();
            CreateNewStageUI(ref stageUI);
            stageUI.SetUI(BuildableGameObject, StateText, BackgroundPanel, TextPrefabCounterItems);
            ProgressProcentSlider.gameObject.SetActive(false);
            currentStageUI = stageUI;
        }
        else if (stageType == typeof(WaitAndGive))
        {
            WaitAndGiveUI stageUI = new WaitAndGiveUI();
            CreateNewStageUI(ref stageUI);
            stageUI.SetUI(BuildableGameObject, StateText, ProgressProcentSlider);
            BackgroundPanel.gameObject.SetActive(false);
            currentStageUI = stageUI;
        }
    }

    private void CreateNewStageUI<T>(ref T stageUI) where T : BuildableObjectUI
    {
        GameObject prefabGameObject = new GameObject();
        GameObject stageUIGameObject = GameObject.Instantiate(prefabGameObject, transform.position, transform.rotation, transform);

        // Create prefab Image for each item's image
        stageUI = stageUIGameObject.AddComponent<T>();
        stageUIGameObject.AddComponent<RectTransform>();

        GameObject.Destroy(prefabGameObject);
    }

    public void OnChangeStage(BuildableObject caller, BuildStageChangeEventArgs e)
    {
        GameObject.Destroy(currentStageUI.gameObject);
       
        SetNewStage(e.buildStage);

        currentStageUI.Activate();
    }

    public void OnBuildStageFinished(BuildableObject caller, BuildStageFinishedEventArgs e)
    {
        GameObject.Destroy(currentStageUI.gameObject);

        isCompleted = true;
    }

    bool CheckCloseToTag()
    {
        GameObject goWithTag = GameObject.FindGameObjectWithTag("Player");

        if (Vector3.Distance(transform.position, goWithTag.transform.position) <= MinimumDistanceToAppear)
            return true;
        return false;
    }
}
