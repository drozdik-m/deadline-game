using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clss creates, activate and deactivate UI for different stages
/// </summary>
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

    /// <summary>
    /// Slider for progress of preparing item
    /// </summary>
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

    /// <summary>
    /// UI object of current stage
    /// </summary>
    private BuildableObjectUI currentStageUI;

    private void Start()
    {
        transformerUICanvas = GetComponent<Canvas>();

        SetNewStage (BuildableGameObject.GetComponent<BuildStageCollection>().stages[0]);
        currentStageUI.Activate();

        BuildableGameObject.GetComponent<BuildableObject>().OnChange += OnChangeStage;
        BuildableGameObject.GetComponent<BuildableObject>().OnFinished += OnBuildStageFinished;
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

    /// <summary>
    /// Sets new Build stage
    /// </summary>
    /// <param name="buildStage">New build stage</param>
    private void SetNewStage(BuildStage buildStage)
    { 
        Type stageType = buildStage.GetType();
        Debug.Log("It is " + stageType.Name);

        if (stageType == typeof(ConsumeItemsStage))
        {
            ConsumeItemsUI stageUI = CreateNewStageUI<ConsumeItemsUI>();
            stageUI.SetUI(BuildableGameObject, StateText, BackgroundPanel, TextPrefabCounterItems);
            currentStageUI = stageUI;
        }
        else if (stageType == typeof(WaitAndGive))
        {
            WaitAndGiveUI stageUI = CreateNewStageUI<WaitAndGiveUI>();
            stageUI.SetUI(BuildableGameObject, StateText, ProgressProcentSlider);
            currentStageUI = stageUI;
        }
        else if (stageType == typeof(ConsumeItemStage))
        {
            ConsumeItemUI stageUI = CreateNewStageUI<ConsumeItemUI>();
            stageUI.SetUI(BuildableGameObject, StateText, BackgroundPanel);
            currentStageUI = stageUI;
        }
        else if (stageType == typeof(NoItemStage))
        {
            NoItemUI stageUI = CreateNewStageUI<NoItemUI>();
            stageUI.SetUI(BuildableGameObject, StateText);
            currentStageUI = stageUI;
        }
        else if (stageType == typeof(PersistentItemStage))
        {
            PersistentItemUI stageUI = CreateNewStageUI<PersistentItemUI>();
            stageUI.SetUI(BuildableGameObject, StateText, BackgroundPanel);
            currentStageUI = stageUI;
        }
    }

    /// <summary>
    /// Creates new Game object for stage UI
    /// </summary>
    /// <typeparam name="T">Type of the object should be BuildableObjectUI.</typeparam>
    private T CreateNewStageUI<T>() where T : BuildableObjectUI
    {
        GameObject prefabGameObject = new GameObject();
        GameObject stageUIGameObject = GameObject.Instantiate(prefabGameObject, transform.position, transform.rotation, transform);

        // Create prefab Image for each item's image
        T stageUI = stageUIGameObject.AddComponent<T>();
        stageUIGameObject.AddComponent<RectTransform>();

        GameObject.Destroy(prefabGameObject);

        return stageUI;
    }

    /// <summary>
    /// Changes current stage UI to the new one and activates it
    /// </summary>
    /// <param name="caller">Caller</param>
    /// <param name="e">Arguments</param>
    public void OnChangeStage(BuildableObject caller, BuildStageChangeEventArgs e)
    {
        currentStageUI.Deactivate();
        GameObject.Destroy(currentStageUI.gameObject);
       
        SetNewStage(e.buildStage);

        currentStageUI.Activate();
    }

    /// <summary>
    /// Sets status of UI stages as completed
    /// </summary>
    /// <param name="caller">Caller</param>
    /// <param name="e">Arguments</param>
    public void OnBuildStageFinished(BuildableObject caller, BuildStageFinishedEventArgs e)
    {
        GameObject.Destroy(currentStageUI.gameObject);

        isCompleted = true;

        Debug.Log("Completed");
    }

    /// <summary>
    /// Checks if game object is close to the player
    /// </summary>
    /// <returns>Return true, if player close enough. Overwise return false.</returns>
    bool CheckCloseToTag()
    {
        GameObject goWithTag = GameObject.FindGameObjectWithTag("Player");

        if (Vector3.Distance(transform.position, goWithTag.transform.position) <= MinimumDistanceToAppear)
            return true;
        return false;
    }
}
