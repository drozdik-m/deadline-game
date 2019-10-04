using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugConsoleController : MonoBehaviour
{
    public InputField commandInput;
    public Text logText;
    public GameObject viewContainer;

    // Start is called before the first frame update
    void Start()
    {
        viewContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("`"))
        {
            viewContainer.SetActive(!viewContainer.activeSelf);

            focusCommandInput();   
        }

        if (viewContainer.activeSelf && Input.GetKeyDown("return"))
        {
            string enteredCommand = commandInput.text;

            // clear input
            commandInput.text = "";

            // add command to log view
            logLine(enteredCommand);

            handleCommand(enteredCommand);

            focusCommandInput();
        }
    }

    void handleCommand(string commandStr)
    {
        Debug.Log("Handle command: '" + commandStr + "'");

        try
        {
            Command command = CommandFactory.GetCommand(commandStr);

            command.Run();

            logLine(command.resultMessage);
        }
        catch (CommandFactoryException e)
        {
            logLine("Error when constructing command: " + e.Message);
        }
        catch (CommandException e)
        {
            logLine("Error when processing command: " + e.Message);
        }
        catch (Exception e)
        {
            logLine("Fatal Error: " + e.Message);
        }
    }

    void focusCommandInput()
    {
        commandInput.Select();
        commandInput.ActivateInputField();
    }

    void logLine(string str)
    {
        logText.text += $"\n{str}";
    }
}
