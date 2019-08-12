using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void WaitAndGiveHanlder(BuildStage source, WaitAndGiveArgs consumeItemsStageArgs);

/// <summary>
/// Represents build stage for what player does not need anything
/// </summary>
[System.Serializable]
public class WaitAndGive : BuildStage
{
    /// <summary>
    /// Optional inventory to check item in
    /// </summary>
    public Inventory overrideInventory;
    /// <summary>
    /// If is the counter finished, then true
    /// </summary>
    public bool CounterFinished = false;
    /// <summary>
    /// The delay of the transformation
    /// </summary>
    [HideInInspector]
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
    /// <summary>
    /// The item provider.
    /// </summary>
    public ItemProvider ItemProvider;

    public override bool ConditionsSatisfied()
    {

        // check if the item is in the inventory
        if (overrideInventory == null)
            overrideInventory = GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>();

        //Check null inventory
        if (overrideInventory == null)
        {
            Debug.LogError("PersistentItemStage: Inventory is null event after trying to find it");
            return false;
        }


        if (CounterFinished)
        {
            OnTransformationFinished?.Invoke(this, new WaitAndGiveArgs(CounterFinished, Delay));
            //Provide set item
            ItemProvider = this.GetComponentInParent<ItemProvider>();
            if (!ItemProvider)
            {
                Debug.LogError("No Item provider found!");
                return false;
            }

            ItemProvider.ProvideItem();

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
