using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quest...
/// </summary>
public class Quest : MonoBehaviour
{
    /// <summary>
    /// Quests condition array
    /// </summary>
    public QuestCondition[] conditions;

    /// <summary>
    /// Quest description (for UI)
    /// </summary>
    public string QuestDescription = "";

    /// <summary>
    /// Event invoked on any condition change
    /// </summary>
    public event QuestHandler OnChange;

    /// <summary>
    /// Should the conditions record?
    /// </summary>
    public bool Recording
    {
        set
        {
            for (int i = 0; i < conditions.Length; i++)
                conditions[i].Recording = true;
        }
    }

    void Start()
    {
        for (int i = 0; i < conditions.Length; i++)
            conditions[i].OnChange += OnConditionChangeCallback;
    }

    /// <summary>
    /// On any condition changed callback
    /// </summary>
    /// <param name="caller"></param>
    /// <param name="args"></param>
    private void OnConditionChangeCallback(QuestCondition caller, QuestConditionArgs args)
    {
        OnChange?.Invoke(this, new QuestArgs(IsCompleted()));
    }

    /// <summary>
    /// Is this quest completed?
    /// </summary>
    /// <returns>True if all condition are met, else false</returns>
    public bool IsCompleted()
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            if (!conditions[i].ConditionMet())
                return false;
        }
        return true;
    }

    

}

public delegate void QuestHandler(Quest caller, QuestArgs args);