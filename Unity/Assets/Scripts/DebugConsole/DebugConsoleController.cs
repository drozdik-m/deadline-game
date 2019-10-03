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
            logText.text += createLogLine(enteredCommand);

            handleCommand(enteredCommand);

            focusCommandInput();
        }
    }

    void handleCommand(string commandStr)
    {
        Debug.Log("Handle command: '" + commandStr + "'");
        //Command command = CommandFactory.GetCommand(commandStr);

        //if (command == null)
        //    logText.text += createLogLine("Command not found. Try command 'help' for list of available commands");

        //command.Run();

        //logText.text += createLogLine(command.ResultMessage);
    }

    void focusCommandInput()
    {
        commandInput.Select();
        commandInput.ActivateInputField();
    }

    string createLogLine(string str)
    {
        return $"\n{str}";
    }
}
