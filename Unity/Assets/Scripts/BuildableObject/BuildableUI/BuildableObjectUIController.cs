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

    /// <summary>
    /// Minimum disstance to the player to appear
    /// </summary>
    public float MinimumDistanceToAppear = 4;

    /// <summary>
    /// Canvas of the Transformer UI object
    /// </summary>
    private Canvas transformerUICanvas;

    private BuildableObjectUI currentStage;

    private void Start()
    {
        transformerUICanvas = GetComponent<Canvas>();
        BuildableGameObject.GetComponent<BuildableObject>().OnChange += OnChangeStage;
        SetNewStage(BuildableGameObject.GetComponent<BuildStageCollection>().stages[0]);
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
        GameObject prefabGameObject = new GameObject();

        if (stageType == typeof(ConsumeItemsStage))
        {
            Debug.Log("It is " + stageType.Name);

            GameObject stageUIGameObject = GameObject.Instantiate(prefabGameObject, transform.position, transform.rotation, transform);

            // Create prefab Image for each item's image
            ConsumeItemsUI stageUI = stageUIGameObject.AddComponent<ConsumeItemsUI>();
            stageUIGameObject.AddComponent<RectTransform>();
            stageUI.SetUI(BuildableGameObject, StateText, transformerUICanvas, BackgroundPanel, TextPrefabCounterItems);

            currentStage = stageUI;
        }

        GameObject.Destroy(prefabGameObject);
    }

    public void OnChangeStage(BuildableObject caller, BuildStageChangeEventArgs e)
    {
        SetNewStage(e.buildStage);
    }

    bool CheckCloseToTag()
    {
        GameObject goWithTag = GameObject.FindGameObjectWithTag("Player");

        if (Vector3.Distance(transform.position, goWithTag.transform.position) <= MinimumDistanceToAppear)
            return true;
        return false;
    }
}
