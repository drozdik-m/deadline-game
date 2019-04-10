using UnityEngine;

/// <summary>
/// Reaction source for event middleman
/// </summary>
public class ReactionEvent : Reaction
{
    public InteractionEventMiddleman intecationEventMiddleman;

    protected override void ImmediateReaction()
    {
        intecationEventMiddleman.IntecationTrigger(this, null);
    }
}

