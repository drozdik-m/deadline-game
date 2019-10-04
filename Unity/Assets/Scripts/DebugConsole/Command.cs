using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    protected string commandParams;

    public string resultMessage { get; protected set; }

    public abstract void Run();
}

public class CommandException : Exception
{
    public CommandException(string message) : base(message) { }
}
