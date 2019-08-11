using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void WaitAndGiveHanlder(BuildStage source, WaitAndGiveArgs consumeItemsStageArgs);

/// <summary>
/// Represents build stage for what player does not need anything
/// </summary>
public class WaitAndGive : BuildStage
{
    /// <summary>
    /// If is the counter finished, then true
    /// </summary>
    public bool CounterFinished = false;
    /// <summary>
    /// The delay of the transformation
    /// </summary>
    public float Delay;
    /// <summary>
    /// Occurs when on transformation started.
    /// </summary>
    public event WaitAndGiveHanlder OnTransformationStarted;
    /// <summary>
    /// Occurs when on transformation finished.
    /// </summary>
    public event WaitAndGiveHanlder OnTransformationFinished;
    /// <summary>
    /// Occurs when the player tries to collect the new item before the process is finished
    /// </summary>
    public event WaitAndGiveHanlder OnCollectBeforeFinishedTry;

    public override bool ConditionsSatisfied()
    {
    
        if (CounterFinished)
        {
            OnTransformationFinished?.Invoke(this, new WaitAndGiveArgs(CounterFinished, Delay));
            //Provide set item
            var itemProvider = this.GetComponentInParent<ItemProvider>();
            if (!itemProvider)
            {
                Debug.LogError("No Item provider found!");
                return false;
            }

            itemProvider.ProvideItem();

            return true;
        }
        else
        {
            OnCollectBeforeFinishedTry?.Invoke(this, new WaitAndGiveArgs(CounterFinished, Delay));
        }

        return false;
    }

    /// <summary>
    /// Starts the counter.
    /// </summary>
    /// <returns>The counter.</returns>
    IEnumerator StartCounter()
    {
        OnTransformationStarted?.Invoke(this, new WaitAndGiveArgs(CounterFinished, Delay));
        yield return new WaitForSeconds(Delay);
        CounterFinished = true;
    }

    public override void Load()
    {
        base.Load();
        StartCoroutine(StartCounter());
    }
}
