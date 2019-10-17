using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Reflection;

/// <summary>
/// Class contains all arguments for each stage UI, which sets in the controller
/// </summary>
public class SetUIArguments
{
    public SetUIArguments(GameObject    buildableGameObject, 
                          RectTransform backgroundPanel,
                          Text          stateText, 
                          Text          textPrefabCounterItems, 
                          Slider        progressProcentSlider)
    {
        BuildableGameObject     = buildableGameObject;
        BackgroundPanel         = backgroundPanel;
        StateText               = stateText;
        TextPrefabCounterItems  = textPrefabCounterItems;
        ProgressProcentSlider   = progressProcentSlider;
    }

    public GameObject BuildableGameObject;
    public RectTransform BackgroundPanel;
    public Text StateText;
    public Text TextPrefabCounterItems;
    public Slider ProgressProcentSlider;
}

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

    /// <summary>
    /// Contains all arguments for each stage UI
    /// </summary>
    private SetUIArguments allArguments;

    private void Start()
    {
        transformerUICanvas = GetComponent<Canvas>();

        allArguments = new SetUIArguments(BuildableGameObject, BackgroundPanel, StateText, TextPrefabCounterItems, ProgressProcentSlider);

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
        currentStageUI = CreateNewStageUI(buildStage);
        currentStageUI.SetUI(allArguments);
    }

    /// <summary>
    /// Creates new Game object for stage UI
    /// </summary>
    private BuildableObjectUI CreateNewStageUI(BuildStage buildStage)
    {
        var prefabGameObject = new GameObject();
        var stageUIGameObject = Instantiate(prefabGameObject, transform.position, transform.rotation, transform);

        // Create var Image for each item's image
        var stageUI = (BuildableObjectUI)stageUIGameObject.AddComponent(buildStage.UIBuildableStageType);
        stageUIGameObject.AddComponent<RectTransform>();

        Destroy(prefabGameObject);

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

        if (e.buildStage)
        {
            SetNewStage(e.buildStage);
            currentStageUI.Activate();
        }
    }

    /// <summary>
    /// Sets status of UI stages as completed
    /// </summary>
    /// <param name="caller">Caller</param>
    /// <param name="e">Arguments</param>
    public void OnBuildStageFinished(BuildableObject caller, BuildStageFinishedEventArgs e)
    {
        GameObject.Destroy(currentStageUI.gameObject);

        gameObject.SetActive(false);

        isCompleted = true;
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
