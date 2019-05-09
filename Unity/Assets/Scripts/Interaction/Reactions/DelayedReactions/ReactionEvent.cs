using UnityEngine;

/// <summary>
/// Reaction source for event middleman
/// </summary>
public class ReactionEvent : DelayedReaction
{
    public InteractionEventMiddleman intecationEventMiddleman;

    protected override void ImmediateReaction()
    {
        intecationEventMiddleman.IntecationTrigger(this, null);
    }
}

