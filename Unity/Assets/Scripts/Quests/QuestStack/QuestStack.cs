using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stack of quests
/// </summary>
public class QuestStack : MonoBehaviour
{
    /// <summary>
    /// Event invoken on any quest change
    /// </summary>
    public event QuestStackHandler OnChange;

    /// <summary>
    /// List of quests
    /// </summary>
    public Quest[] quests = new Quest[0];

    /// <summary>
    /// Quests will no longer be reconding once all of them are completed
    /// </summary>
    public bool FreezeOnComplete = true;

    /// <summary>
    /// Should the quests record?
    /// </summary>
    public bool Recording
    {
        set
        {
            for (int i = 0; i < quests.Length; i++)
                quests[i].Recording = value;
        }
    }

    private void Start()
    {
        for (int i = 0; i < quests.Length; i++)
            quests[i].OnChange += OnQuestChangeCallback;
    }

    /// <summary>
    /// Quest change callback
    /// </summary>
    /// <param name="caller"></param>
    /// <param name="args"></param>
    private void OnQuestChangeCallback(Quest caller, QuestArgs args)
    {
        OnChange?.Invoke(this, new QuestStackArgs(QuestsAreCompleted()));

        if (FreezeOnComplete && QuestsAreCompleted())
            Recording = false;
    }

    /// <summary>
    /// Checks if all quests are completed
    /// </summary>
    /// <returns>True if all quests are completed, else false</returns>
    public bool QuestsAreCompleted()
    {
        for (int i = 0; i < quests.Length; i++)
            if (!quests[i].IsCompleted())
                return false;
        return true;
    }
}

public delegate void QuestStackHandler(QuestStack caller, QuestStackArgs args);
