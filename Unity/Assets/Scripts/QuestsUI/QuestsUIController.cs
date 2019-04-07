using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestsUIController : MonoBehaviour
{
    public QuestStack QuestsStorage;

    public QuestUI[] QuestsUIStorage;

    // Start is called before the first frame update
    void Start()
    {
        if (QuestsStorage != null)
            QuestsStorage.OnChange += ChangeQuestsUI;

        ActivateQuestsUI ();
    }

    void ChangeQuestsUI(QuestStack questStack, QuestStackArgs args)
    {
        int cnt = 0;

        foreach (Quest quest in questStack.quests)
        {
            if (quest.IsCompleted ())
                QuestsUIStorage[cnt].SetNewUI (quest.QuestDescription, Color.green, 0.5f);
            else
                QuestsUIStorage[cnt].SetNewUI (quest.QuestDescription, Color.red, 1.0f);

            cnt++;
        }
    }

    public void SetQuestStorage(QuestStack newQuestStorage)
    {
        QuestsStorage = newQuestStorage;
        QuestsStorage.OnChange += ChangeQuestsUI;

        ActivateQuestsUI ();
    }

    public void ActivateQuestsUI()
    {
        int i;
        for (i = 0; i < QuestsStorage.quests.GetLength(0); i++)
        {
            QuestsUIStorage[i].gameObject.SetActive (true);
        }

        for (int j = i; j < QuestsUIStorage.GetLength(0); j++)
        {
            QuestsUIStorage[j].gameObject.SetActive (false);
        }
    }
}
