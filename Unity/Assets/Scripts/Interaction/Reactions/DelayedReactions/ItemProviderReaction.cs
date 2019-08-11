using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProviderReaction : DelayedReaction
{
    public ItemProvider itemProvider;

    protected override void ImmediateReaction()
    {
        itemProvider.ProvideItem();
    }
}
