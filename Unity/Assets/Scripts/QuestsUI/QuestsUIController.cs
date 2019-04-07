using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsUIController : MonoBehaviour
{
    public QuestStack QuestStorage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetQuestStorage(QuestStack newQuestStorage )
    {
        QuestStorage = newQuestStorage;
    }
}
