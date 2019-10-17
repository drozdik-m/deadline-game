using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpDCComm : DCComm
{
    public override void Run()
    {
        resultMessage = "changeScene <scene_name> - changes scene to <scene_name> (alias 'changeLevel')\n" +
                        "restartScene - restarts current scene (alias 'restartLevel')\n" +
                        "listScenes - lists all available scenes (alias 'listLevels')\n" +
                        "clear - clears the console\n" +
                        "help - displays this help message\n" +
                        "---";
    }
}
