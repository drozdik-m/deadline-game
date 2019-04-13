using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manually triggered quest condition
/// </summary>
public class QuestManualCondition : QuestCondition
{
    /// <summary>
    /// Set complete status
    /// </summary>
    public bool Completed
    {
        get
        {
            return completed;
        }
        set
        {
            if (!Recording)
                return;
            completed = value;
            ConditionChanged(completed);
        }
    }

    /// <summary>
    /// Is the condition complete?
    /// </summary>
    [SerializeField]
    private bool completed = false;

    public override bool ConditionMet()
    {
        return Completed;
    }
}
