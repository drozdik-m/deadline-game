using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Command that changes scenes
/// </summary>
public class ChangeLevelDCComm : DCComm
{
    public ChangeLevelDCComm(string commandParams)
    {
        this.commandParams = commandParams;
    }

    public override void Run()
    {
        // find scenes workflow component
        ScenesWorkflow[] objectsWithScenesWorkflowComponent = Object.FindObjectsOfType<ScenesWorkflow>();

        if (objectsWithScenesWorkflowComponent == null || objectsWithScenesWorkflowComponent.Length < 1)
            throw new CommandException("Error: No 'ScenesWorkflowComponent' found");
            
        ScenesWorkflow scenesWorkflow = objectsWithScenesWorkflowComponent[0];

        // check if provided scene exists
        bool found = false;
        foreach (WorkflowScene scene in scenesWorkflow.StoryScenes)
        {
            if (scene.SceneName == commandParams)
            {
                found = true;
                break;
            }
        }

        if (found)
        {
            scenesWorkflow.ChangeScene(commandParams);

            // mark this command as command that is manipulating with scene
            manipulatingScene = true;
        }
        else
            throw new CommandException($"Scene '{commandParams}' not found. \n" +
                 "Make sure it is added in 'StoryScenes' of 'ScenesWorkflow' component.\n" +
                 "To get list of available scenes, try command 'listLevels'");
    }
}
