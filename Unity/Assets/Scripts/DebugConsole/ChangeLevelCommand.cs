using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevelCommand : Command
{
    public ChangeLevelCommand(string commandParams)
    {
        this.commandParams = commandParams;
    }

    public override void Run()
    {
        Debug.Log("Change Level Command");
    }
}
