using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    public Text nameTextField;
    public Text dialogTextField;

    private string characterName;
    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();    
    }

    public void startDialog(SelfTalkDialog dialog)
    {
        sentences.Clear();

        foreach (var sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        characterName = dialog.characterName;

        nextSentence();
    }

    public void nextSentence()
    {
        if (sentences.Count == 0)
        {
            endOfDialog();
            return;
        }

        string sentenceToDisplay = sentences.Dequeue();

        nameTextField.text = characterName;
        dialogTextField.text = sentenceToDisplay;
    }

    void endOfDialog()
    {
        characterName = "";
        Debug.Log("End of dialog!");
    }
}
