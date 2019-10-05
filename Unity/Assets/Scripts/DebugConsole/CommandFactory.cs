using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CommandFactory creates right command based on passed string version of the command (and parameters)
/// </summary>
public static class CommandFactory
{
    public static Command GetCommand(string commandStr, string cparams)
    {
        switch (commandStr)
        {
            case "changeScene":
            case "changeLevel":
                return new ChangeLevelCommand(cparams);

            case "restartScene":
            case "restartLevel":
                if (!string.IsNullOrWhiteSpace(cparams))
                    throw new CommandFactoryException("Restart level command cannot take arguments");
                return new RestartLevelCommand();

            case "listScenes":
            case "listLevels":
                return new ListScenesCommand();
                
            case "clear":
                return new ClearCommand();

            case "help":
                return new HelpCommand();

            default:
                throw new CommandFactoryException("Command not found");
        }
    }
}

/// <summary>
/// Exception for catching command factory assining errors
/// </summary>
public class CommandFactoryException : Exception
{
    public CommandFactoryException(string message) : base(message) { }
}
