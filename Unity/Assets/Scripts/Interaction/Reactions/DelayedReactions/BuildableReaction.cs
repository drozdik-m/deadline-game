using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableReaction : DelayedReaction
{
    public BuildableObject buildableObject;

    protected override void ImmediateReaction()
    {
        buildableObject.AttemptNextStage();
    }
}
