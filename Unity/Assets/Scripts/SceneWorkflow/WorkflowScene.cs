using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// One scene that is part of story/gameplay flow
/// </summary>
public class WorkflowScene : MonoBehaviour
{
    /// <summary>
    /// Scene name - Must be the same as scene file name
    /// </summary>
    public string SceneName = "";

    /// <summary>
    /// Scene stage manager. 
    /// </summary>
    public StageManager MainStageManger = null; 

    /// <summary>
    /// Scene music theme
    /// </summary>
    public BackgroundMusicTheme StageMusicTheme = BackgroundMusicTheme.NoMusic;

    /// <summary>
    /// Scene transition-out color
    /// </summary>
    public Color NextSceneTransitionColor = Color.black;

    /// <summary>
    /// Scene transition speed
    /// </summary>
    public float NextSceneTransitionSpeed = 2f;

    /// <summary>
    /// Is this scene ready for the next scene?
    /// </summary>
    /// <returns></returns>
    public bool ReadyForNextScene()
    {
        if (MainStageManger == null)
            return false;
        else if (MainStageManger.IsFinished())
            return true;
        return false;
    }


    /// <summary>
    /// Initiates scene (should be called once on load)
    /// </summary>
    public void InitiateScene()
    {
        initiate = true;
    }

    private bool initiate = false;
    private void InitiateSceneLoader()
    {
        GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<BackgroundMusic>()
            .ChangeTheme(StageMusicTheme, NextSceneTransitionSpeed);

        if (MainStageManger == null)
        {
            //Try to find it
            GameObject manager = GameObject.FindGameObjectWithTag("MainStageManager");
            if (manager != null)
                MainStageManger = manager.GetComponent<StageManager>();
        }

        if (MainStageManger != null)
        {
            MainStageManger.OnStageChange -= OnMainStageManagerChange;
            MainStageManger.OnStageChange += OnMainStageManagerChange;
            MainStageManger.InitiateStages();

        }
    }

    private void Update()
    {
        if (initiate)
        {
            InitiateSceneLoader();
            initiate = false;
        }
    }

    private void OnMainStageManagerChange(StageManager caller, StageManagerArgs args)
    {
        if (args.CurrentStage.GetType() == typeof(StageQuestStack))
        {
            var masterUI = GameObject.FindGameObjectWithTag("MasterUI");
            if (masterUI != null)
            {
                masterUI.GetComponent<UIMaster>().QuestUI.
                    SetQuestStack(((StageQuestStack)args.CurrentStage).QuestStack);
            }
            else
                Debug.LogWarning("MasterUI not found");
        }
    }

    /// <summary>
    /// Changes current scene to another scene
    /// </summary>
    /// <param name="sceneName">New scene file name</param>
    public void ChangeSceneTo(string sceneName)
    {
        GameObject.FindGameObjectWithTag("InvincibleObject").GetComponent<ScenesChangeManager>()
            .ChangeScene(sceneName, NextSceneTransitionColor, NextSceneTransitionSpeed);
    }
}
