using UnityEngine;

/// <summary>
/// Represents Reaction abstract class.
/// </summary>
public abstract class Reaction : MonoBehaviour
{
    /// <summary>
    /// initialize the reaction
    /// </summary>
    public void Init()
    {
        SpecificInit();
    }

    /// <summary>
    /// specific initialization functionality for more complex reactions
    /// </summary>
    protected virtual void SpecificInit() { }

    /// <summary>
    /// play reaction
    /// </summary>
    /// <param name="monoBehaviour"></param>
    public void React(MonoBehaviour monoBehaviour)
    {
        ImmediateReaction();
    }

    /// <summary>
    /// specific functionality for playing reaction
    /// </summary>
    protected abstract void ImmediateReaction();
}