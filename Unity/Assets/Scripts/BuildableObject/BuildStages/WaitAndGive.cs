using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents build stage for what player does not need anything
/// </summary>
public class WaitAndGive : BuildStage
{

    public bool CounterFinished { get; set; } = false;

    public override bool ConditionsSatisfied()
    {
        if (CounterFinished)
        {
            //Provide set item

            return true;
        }

        return false;
    }

    public override void Init()
    {
        base.Init();
        StartCounter();
    }

    public void StartCounter()
    {
        //Set CounterFinished to true after time [ms]
    }
}
