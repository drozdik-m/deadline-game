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
    /// Prefab of the QuestUIPanel
    /// </summary>
    public QuestUI QuestUIPanelPrefab;

    /// <summary>
    /// Alpha of the quest text description if it's completed
    /// </summary>
    [Range(0.0f, 1.0f)]
    public float NotActiveTextAlpha;

    /// <summary>
    /// Current QuestStack were all quests are stored
    /// </summary>
    private QuestStack currentQuestStorage;

    /// <summary>
    /// Array of the quests representation on the UI quests
    /// </summary>
    private QuestUI[] questUIStorage;

    /// <summary>
    /// Distance between items in the UI quests
    /// </summary>
    private int positionOffset = 40;

    /// <summary>
    /// Changes quest UI
    /// </summary>
    /// <param name="questStack">QuestStack that called the function</param>
    /// <param name="args">Parametrs of the QuestStack</param>
    public void ChangeQuestsUI(QuestStack questStack, QuestStackArgs args)
    {
        UpdateQuestUI ();
    }

    /// <summary>
    /// Sets new QuestStack and update UI quests
    /// </summary>
    /// <param name="newQuestStack">New QuestStack</param>
    public void SetQuestStack(QuestStack newQuestStack)
    {
        if (currentQuestStorage != null)
            currentQuestStorage.OnChange -= ChangeQuestsUI;

        currentQuestStorage = newQuestStack;
        currentQuestStorage.OnChange += ChangeQuestsUI;

        ReactivateQuestUI ();
        UpdateQuestUI ();
    }

    /// <summary>
    /// Create new items of the UI quests and destory the old one
    /// </summary>
    private void ReactivateQuestUI()
    {
        // Destroy all old quests  
        if (questUIStorage != null)
        {
            foreach (QuestUI questUI in questUIStorage)
            {
                Destroy (questUI.gameObject);
            }
        }

        questUIStorage = new QuestUI[currentQuestStorage.quests.Length];

        for (int i = 0; i < currentQuestStorage.quests.Length; i++)
        {
            Vector3 pos = transform.position;
            pos.y -= (i + 1) * positionOffset;
            questUIStorage[i] = QuestUI.Instantiate (QuestUIPanelPrefab, pos, transform.rotation, transform);
        }
    }

    /// <summary>
    /// Updates UI quests depending on the status of the quest
    /// </summary>
    private void UpdateQuestUI()
    {
        for (int i = 0; i < currentQuestStorage.quests.Length; i++)
        {
            Quest quest = currentQuestStorage.quests[i];
            if (quest.IsCompleted ())
                questUIStorage[i].SetNewUI (quest.QuestDescription, Color.green, NotActiveTextAlpha);
            else
                questUIStorage[i].SetNewUI (quest.QuestDescription, Color.red, 1.0f);
        }
    }
}
