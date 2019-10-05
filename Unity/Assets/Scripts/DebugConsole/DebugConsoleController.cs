using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Debug Console Controller is the middle man between console input and command logic
/// </summary>
public class DebugConsoleController : MonoBehaviour
{
    /// <summary>
    /// Input for user to type commands
    /// </summary>
    public InputField commandInput;

    /// <summary>
    /// Console output
    /// </summary>
    public Text logText;

    /// <summary>
    /// Container of console
    /// </summary>
    public GameObject viewContainer;

    /// <summary>
    /// Name of the GameObject that was lagged last time
    /// </summary>
    string lastBuggedItem;

    void Start()
    {
        // hide console when start
        viewContainer.SetActive(false);

        // subscribe to bug resolver event
        BugResolver[] bugResolvers = FindObjectsOfType<BugResolver>();
        if (bugResolvers == null || bugResolvers.Length < 1)
            logLine("Bug Resolver not found -> could not subscribe to Bug Resolver");
        else
            bugResolvers[0].OnObjectBugged +=
            (BugResolver br, BugResolverArgs bra) =>
            {
                // avoid logging one item over and over again
                if (lastBuggedItem != bra.BuggedItemGameObject.name)
                {
                    logLine($"BugResolver: GameObject '{bra.BuggedItemGameObject.name}' is bugged");
                    lastBuggedItem = bra.BuggedItemGameObject.name;
                }
            };

        // subscribe to all messages from unity
        Application.logMessageReceived += (string cond, string stackTrace, LogType type) =>
        {
            logLine("Unity Exception: " + cond);
            logLine(stackTrace);
        };
    }

    void Update()
    {
        // on '`' pressed, show console and focus input
        if (Input.GetKeyDown("`"))
        {
            viewContainer.SetActive(!viewContainer.activeSelf);
            focusCommandInput();   
        }

        // register 'enter' on shown console to catch commands
        if (viewContainer.activeSelf && Input.GetKeyDown("return"))
        {
            string enteredCommand = commandInput.text;

            // clear input
            commandInput.text = "";

            // parse command to two parts: command string and parameters
            if (!parseEnteredCommand(enteredCommand, out string onlyCommand, out string onlyParams))
                logLine("Error: Command is invalid, could not be parsed");

            // add command to log view
            if (string.IsNullOrWhiteSpace(onlyCommand))
                logLine("");
            else
            {
                logLine("*" + onlyCommand + " " + onlyParams + "*");
                handleCommand(onlyCommand, onlyParams);
            }
                

            focusCommandInput();
        }
    }

    // handles command based on string
    void handleCommand(string commandStr, string cparams)
    {
        try
        {
            Command command = CommandFactory.GetCommand(commandStr, cparams);

            if (command is ClearCommand)
                logText.text = "";
            else
                command.Run();

            logLine(command.resultMessage);
        }
        catch (CommandFactoryException e)
        {
            logLine("Error: " + e.Message);
        }
        catch (CommandException e)
        {
            logLine("Error: " + e.Message);
        }
        catch (Exception e)
        {
            logLine("Fatal Error: " + e.Message);
        }
    }

    /// <summary>
    /// Focuses input for typing command
    /// </summary>
    void focusCommandInput()
    {
        commandInput.Select();
        commandInput.ActivateInputField();
    }

    /// <summary>
    /// Logs line in console output
    /// </summary>
    /// <param name="str">The addition to the console output</param>
    void logLine(string str)
    {
        logText.text += $"\n{str}";
    }

    /// <summary>
    /// Takes entered row from user and divides it into two parts: command and parameters
    /// </summary>
    /// <param name="commandStr">Whole command row from user</param>
    /// <param name="onlyCommand">Entered command</param>
    /// <param name="onlyParams">Entered parameters</param>
    /// <returns></returns>
    bool parseEnteredCommand(string commandStr, out string onlyCommand, out string onlyParams)
    {
        onlyCommand = onlyParams = "";

        if (string.IsNullOrWhiteSpace(commandStr)) return true;

        // remove starting spacing (allowing commands like "    restartLevel")
        commandStr = remStartingSpaces(commandStr);

        // read the command until the first space
        commandStr = readUntilSpace(commandStr, out onlyCommand);

        if (string.IsNullOrWhiteSpace(onlyCommand)) return false;

        // remove spaces between onlyCommand and onlyParams (allowing commands like "changeLevel               <scene_name>")
        onlyParams = remStartingSpaces(commandStr);

        return true;
    }

    /// <summary>
    /// Reads string until space occures
    /// </summary>
    /// <param name="commandStr">Input for reading</param>
    /// <param name="onlyCommand">Result</param>
    /// <returns>commandStr without read substring</returns>
    string readUntilSpace(string commandStr, out string onlyCommand)
    {
        int counter = 0;
        foreach (char c in commandStr)
        {
            if (c != ' ')
                ++counter;
            else
                break;
        }

        if (counter == commandStr.Length)
        {
            onlyCommand = commandStr;
            return "";
        }
        else
        {
            onlyCommand = commandStr.Substring(0, counter);
            return commandStr.Substring(counter);
        }
    }

    /// <summary>
    /// Removes starting spaces from string
    /// </summary>
    /// <param name="commandStr">Input for removing</param>
    /// <returns>commandStr without read substring of spaces</returns>
    string remStartingSpaces(string commandStr)
    {
        int counter = 0;
        foreach (char c in commandStr)
        {
            if (c == ' ')
                ++counter;
            else
                break;
        }

        if (counter == commandStr.Length)
            return "";
        else
            return commandStr.Substring(counter);
    }
}
