using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manually triggered quest condition
/// </summary>
public class QuestManualCondition : QuestCondition
{
    public bool Completed
    {
        get
        {
            return completed;
        }
        set
        {
            completed = value;
            ConditionChanged(completed);
        }
    }

    [SerializeField]
    private bool completed = false;

    public override bool ConditionMet()
    {
        return Completed;
    }
}
