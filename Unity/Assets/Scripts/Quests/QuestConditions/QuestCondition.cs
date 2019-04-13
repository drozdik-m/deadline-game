using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class represents a condition. Quests can have mutiple conditions.
/// </summary>
public abstract class QuestCondition : MonoBehaviour
{
    /// <summary>
    /// Event invoked when any condition status has changed
    /// </summary>
    public event QuestConditionHandler OnChange;

    /// <summary>
    /// Checks if condition is met
    /// </summary>
    /// <returns>True if condition is met, else false</returns>
    public abstract bool ConditionMet();


    /// <summary>
    /// Should the condition record?
    /// </summary>
    public bool Recording
    {
        get
        {
            return recording;
        }
        set
        {
            recording = value;
        }
    }

    /// <summary>
    /// Is the condition recording?
    /// </summary>
    [SerializeField]
    bool recording = false;

    /// <summary>
    /// Triggeres important events, acts as easy access for child classes. Call when condition status changed.
    /// </summary>
    /// <param name="completed">Are conditions completed/met?</param>
    protected void ConditionChanged(bool completed)
    {
        OnChange?.Invoke(this, new QuestConditionArgs(completed));
    }
}


public delegate void QuestConditionHandler(QuestCondition caller, QuestConditionArgs args);