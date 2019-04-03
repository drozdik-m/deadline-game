using System.Collections;
using UnityEngine;

/// <summary>
/// Represents abstract class for reactions that need to be delayed.
/// </summary>
public abstract class DelayedReaction : Reaction
{
    /// <summary>
    /// number of seconds that will be used for delay
    /// </summary>
    public float delay;

    /// <summary>
    /// coroutine for waiting
    /// </summary>
    protected WaitForSeconds wait;

    /// <summary>
    /// initialize coroutine for waiting
    /// </summary>
    public new void Init()
    {
        wait = new WaitForSeconds(delay);
    }

    /// <summary>
    /// play reaction after specified amount of time
    /// </summary>
    /// <param name="monoBehaviour"></param>
    public new void React(MonoBehaviour monoBehaviour)
    {
        monoBehaviour.StartCoroutine(ReactCoroutine());
    }

    /// <summary>
    /// react coroutine that handles the wait
    /// </summary>
    /// <returns></returns>
    protected IEnumerator ReactCoroutine()
    {
        yield return wait;
        ImmediateReaction();
    }

}
