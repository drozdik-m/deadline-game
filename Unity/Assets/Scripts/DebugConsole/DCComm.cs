using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Command class represents specific functionality usable in debug console
/// DCComm stands dor 'Debug Console Command'
/// </summary>
public abstract class DCComm
{
    /// <summary>
    /// Parameters for command
    /// </summary>
    protected string commandParams;

    /// <summary>
    /// Command result message set after Run method is completed
    /// </summary>
    public string resultMessage { get; protected set; }

    /// <summary>
    /// Command functionality itself
    /// </summary>
    public abstract void Run();
}

/// <summary>
/// Exception for catching command inner functionality errors
/// </summary>
public class CommandException : Exception
{
    public CommandException(string message) : base(message) { }
}
