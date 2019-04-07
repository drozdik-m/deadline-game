using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestsUIController : MonoBehaviour
{
    public QuestStack QuestStorage;

    [System.Serializable]
    public struct QuestStatus
    {
        public Text QuestDescriptionText;
        public Image QuestStatusImage;
    }

    public QuestStatus[] QuestsUI;

    // Start is called before the first frame update
    void Start()
    {
        if (QuestStorage != null)
            QuestStorage.OnChange += ChangeQuestsUI;
    }

    void ChangeQuestsUI(QuestStack questStack, QuestStackArgs args)
    {
        Debug.Log ("Change UI quests status");

        foreach (Quest quest in questStack.quests)
        {
            if (quest.IsCompleted ())
            {
                /// TODO
            }
            else
            {
                /// TODO
            }
        }
    }

    public void SetQuestStorage(QuestStack newQuestStorage)
    {
        QuestStorage = newQuestStorage;
        QuestStorage.OnChange += ChangeQuestsUI;
    }
}
