using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene workflow, that handles loading and automatic switching
/// </summary>
public class ScenesWorkflow : MonoBehaviour
{
    /// <summary>
    /// All used scenes
    /// </summary>
    public WorkflowScene[] StoryScenes;

    /// <summary>
    /// Current scene. Null if no current scene loaded or new scene is being loaded
    /// </summary>
    WorkflowScene currentScene = null;

    /// <summary>
    /// Current scene index in scenes array
    /// </summary>
    int currentSceneIndex = 0;

    private void Start()
    {
        RefreshCurrentScene();
        currentScene.InitiateScene();
        SceneManager.sceneLoaded += OnAnySceneLoaded;
    }

    /// <summary>
    /// Event called on any scene loaded. Handles refreshing and automatic init.
    /// </summary>
    /// <param name="scene">Current scene</param>
    /// <param name="sceneMode">Scene mode</param>
    private void OnAnySceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        RefreshCurrentScene();
        currentScene.InitiateScene();
    }

    private void Update()
    {
        if (currentScene == null)
            return;

        if (currentScene.ReadyForNextScene())
            NextScene();
    }

    /// <summary>
    /// Looks for current scene in scenes array based on currently loaded scene name
    /// </summary>
    public void RefreshCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        for (int i  = 0; i < StoryScenes.Length; i++)
        {
            if (StoryScenes[i].SceneName == currentSceneName)
            {
                currentScene = StoryScenes[i];
                currentSceneIndex = i;
                return;
            }
        }

        throw new KeyNotFoundException("Current scene not found in StoryScenes array");
    }

    /// <summary>
    /// Loads next scene in array
    /// </summary>
    public void NextScene()
    {
        if (currentScene == null)
            return;
        if (!HasNextScene())
            return;

        currentScene.ChangeSceneTo(StoryScenes[currentSceneIndex + 1].SceneName);
        currentScene = null;
    }

    /// <summary>
    /// Tells if there is any next scene
    /// </summary>
    /// <returns>True if there is any next scene, else false</returns>
    private bool HasNextScene()
    {
        if (currentScene == null)
            return false;
        if (currentSceneIndex >= 0 && currentSceneIndex < StoryScenes.Length - 1)
            return true;
        return false;
    }

    /// <summary>
    /// Changes to any other scene. The scene must be in scenes array.
    /// </summary>
    /// <param name="sceneName">File name of new scene</param>
    public void ChangeScene(string sceneName)
    {
        if (currentScene == null)
            SceneManager.LoadScene(sceneName);
        else
            currentScene.ChangeSceneTo(sceneName);
    }
}
