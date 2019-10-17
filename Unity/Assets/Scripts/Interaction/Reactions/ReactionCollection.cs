using UnityEngine;

/// <summary>
/// Represents set of reactions that will be coupled together to be played one after another.
/// </summary>
public class ReactionCollection : MonoBehaviour
{
    /// <summary>
    /// Reactions in the set
    /// </summary>
    public Reaction[] reactions = new Reaction[0];

    /// <summary>
    /// Initialization of the reactions
    /// </summary>
    void Start()
    {
        for (int i = 0; i < reactions.Length; i++)
        {
            DelayedReaction delayedReaction = reactions[i] as DelayedReaction;
            if (delayedReaction)
                delayedReaction.Init();
            else
                reactions[i].Init();
        }
    }

    /// <summary>
    /// Let all reactions react
    /// </summary>
    public void React()
    {
        for (int i = 0; i < reactions.Length; i++)
        {
            DelayedReaction delayedReaction = reactions[i] as DelayedReaction;

            if (delayedReaction)
            {
                //Debug.Log("Delayed reaction react()");
                delayedReaction.React(this);
            }
            else
            {
                //Debug.Log("Immediate reaction react()");
                reactions[i].React(this);
            }
                
        }
    }
}
