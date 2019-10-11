using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChangeHandlerArgs
{
    public SkyboxController.Time NewDayTime;

    public SkyboxChangeHandlerArgs(SkyboxController.Time dayTime)
    {
        this.NewDayTime = dayTime;
    }
}
