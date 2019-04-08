using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class controls changes of the UI quests
/// </summary>
public class QuestsUIController : MonoBehaviour
{
    /// <summary>
    /// Current QuestStack were all quests are stored
    /// </summary>
    public QuestStack QuestStorage;

    /// <summary>
    /// Array of the quests representation on the UI quests
    /// </summary>
    public QuestUI[] QuestUIStorage;

    // Start is called before the first frame update
    void Start()
    {
        if (QuestStorage != null)
            QuestStorage.OnChange += ChangeQuestsUI;

        ReactivateQuestUI ();
        UpdateQuestUI (QuestStorage.quests);
    }

    /// <summary>
    /// Changes quest UI
    /// </summary>
    /// <param name="questStack">QuestStack that called the function</param>
    /// <param name="args">Parametrs of the QuestStack</param>
    public void ChangeQuestsUI(QuestStack questStack, QuestStackArgs args)
    {
        try
        {
            UpdateQuestUI (questStack.quests);
        }
        catch (System.Exception exp)
        {
            throw exp;
        }
    }

    /// <summary>
    /// Sets new QuestStack and update UI quests
    /// </summary>
    /// <param name="newQuestStack">New QuestStack</param>
    public void SetQuestStack(QuestStack newQuestStack)
    {
        QuestStorage = newQuestStack;
        QuestStorage.OnChange += ChangeQuestsUI;

        try
        {
            ReactivateQuestUI ();
            UpdateQuestUI (QuestStorage.quests);
        }
        catch (System.Exception exp)
        {
            throw exp;
        }

    }

    /// <summary>
    /// Activates all necessary items of the UI quests
    /// and deactivates unnecessary
    /// </summary>
    private void ReactivateQuestUI()
    {
        int i;
        for (i = 0; i < QuestStorage.quests.Length; i++)
        {
            QuestUIStorage[i].gameObject.SetActive (true);
        }

        for (int j = i; j < QuestUIStorage.Length; j++)
        {
            QuestUIStorage[j].gameObject.SetActive (false);
        }
    }

    /// <summary>
    /// Updates UI quests depending on the status of the quest
    /// </summary>
    /// <param name="quests">Array of the quests</param>
    private void UpdateQuestUI(Quest[] quests)
    {
        int cnt = 0;

        foreach (Quest quest in quests)
        {
            if (quest.IsCompleted ())
                QuestUIStorage[cnt].SetNewUI (quest.QuestDescription, Color.green, 0.5f);
            else
                QuestUIStorage[cnt].SetNewUI (quest.QuestDescription, Color.red, 1.0f);

            cnt++;
        }
    }
}
