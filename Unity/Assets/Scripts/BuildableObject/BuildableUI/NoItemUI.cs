using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoItemUI : BuildableObjectUI
{
    public override void Activate()
    {
        UpdateStateText("Ready");
    }

    public override void Deactivate()
    {

    }
}
