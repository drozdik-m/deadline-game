using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommandFactory
{
    private static bool parseCommandStr(string commandStr, out string onlyCommand, out string onlyParams)
    {
        onlyCommand = onlyParams = "";

        if (string.IsNullOrWhiteSpace(commandStr)) return false;

        // remove starting spacing (allowing commands like "    restartLevel")
        commandStr = remStartingSpaces(commandStr);

        // read the command until the first space
        commandStr = readUntilSpace(commandStr, out onlyCommand);

        if (string.IsNullOrWhiteSpace(onlyCommand)) return false;

        // remove spaces between onlyCommand and onlyParams (allowing commands like "changeLevel               <scene_name>")
        onlyParams = remStartingSpaces(commandStr);

        return true;
    }

    private static string readUntilSpace(string commandStr, out string onlyCommand) => throw new NotImplementedException();
    private static string remStartingSpaces(string commandStr) => throw new NotImplementedException();

    public static Command GetCommand(string commandStr)
    {
        string onlyCommand, onlyParams;

        if (!parseCommandStr(commandStr, out onlyCommand, out onlyParams))
            throw new CommandFactoryException("Command is invalid, could not be parsed");

        switch (onlyCommand)
        {
            case "changeLevel":
                return new ChangeLevelCommand(onlyParams);
            case "restartLevel":
                return new RestartLevelCommand();
            default:
                throw new CommandFactoryException("Command could not be found");
        }
    }
}

public class CommandFactoryException : Exception
{
    public CommandFactoryException(string message) : base(message) { }
}
