using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadUIReaction : DelayedReaction
{
    public ReadableObjectUI readableObjectUI;

    protected override void ImmediateReaction()
    {
        readableObjectUI.Open();
    }
}
