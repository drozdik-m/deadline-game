using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Command for restaring current scene
/// </summary>
public class RestartLevelCommand : Command
{
    public override void Run()
    {
        // find scenes workflow component
        ScenesWorkflow[] objectsWithScenesWorkflowComponent = Object.FindObjectsOfType<ScenesWorkflow>();

        if (objectsWithScenesWorkflowComponent == null || objectsWithScenesWorkflowComponent.Length < 1)
            throw new CommandException("Error: No 'ScenesWorkflowComponent' found");

        // change scene to the current scene -> restart scene
        objectsWithScenesWorkflowComponent[0].ChangeScene(SceneManager.GetActiveScene().name);
        resultMessage = "Level (scene) restarted";
    }
}
