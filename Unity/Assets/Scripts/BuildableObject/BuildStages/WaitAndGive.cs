using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents build stage for what player does not need anything
/// </summary>
public class WaitAndGive : BuildStage
{
    public bool CounterFinished = false;

    public float Delay;

    private bool counterStarted = false;

    public override bool ConditionsSatisfied()
    {

        if (!counterStarted)
        {
            Debug.Log("NEXT");
            counterStarted = true;
            StartCounter();

        }
        if (CounterFinished)
        {
            Debug.Log("PROVIDE");
            //Provide set item
            var itemProvider = this.gameObject.GetComponent<ItemProvider>();
            if (!itemProvider)
            {
                Debug.LogError("No Item provider found!");
                return false;
            }

            itemProvider.ProvideItem();

            return true;
        }

        return false;
    }


    IEnumerator StartCounter()
    {
        Debug.Log("yeild");
        yield return new WaitForSeconds(Delay);
        CounterFinished = true;
    }
}
