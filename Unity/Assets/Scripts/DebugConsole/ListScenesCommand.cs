using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Command for listing all available scenes
/// </summary>
public class ListScenesCommand : Command
{
    public override void Run()
    {
        // find scenes workflow component
        ScenesWorkflow[] objectsWithScenesWorkflowComponent = Object.FindObjectsOfType<ScenesWorkflow>();

        if (objectsWithScenesWorkflowComponent == null || objectsWithScenesWorkflowComponent.Length < 1)
            throw new CommandException("Error: No 'ScenesWorkflowComponent' found");

        // print available scenes in result
        resultMessage += "Available scenes (levels):";
        foreach (WorkflowScene scene in objectsWithScenesWorkflowComponent[0].StoryScenes)
            resultMessage += "\n" + scene.SceneName;
    }
}
