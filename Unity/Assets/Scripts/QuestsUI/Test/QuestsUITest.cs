using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsUITest : MonoBehaviour
{
    public QuestsUIController QuestController;
    public QuestStack Stack1;
    public QuestStack Stack2;

    private QuestStack actualStack;

    // Start is called before the first frame update
    void Start()
    {
        actualStack = Stack1;
    }

    public void ChangeQuestStack()
    {
        if (actualStack == Stack1)
            actualStack = Stack2;
        else
            actualStack = Stack1;

        QuestController.SetQuestStack (actualStack);
    }
}
