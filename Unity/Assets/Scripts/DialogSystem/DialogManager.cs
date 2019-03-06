using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Dialog manager.
/// </summary>
public class DialogManager : MonoBehaviour
{
    /// <summary>
    /// Reference to UI textfield for name.
    /// </summary>
    public Text nameTextField;
    /// <summary>
    /// Refference to UI texfield for sentences.
    /// </summary>
    public Text dialogTextField;
    /// <summary>
    /// Character name to display.
    /// </summary>
    private string characterName;
    /// <summary>
    /// Queue of sentences (dialog).
    /// </summary>
    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();    
    }

    /// <summary>
    /// Starts the dialog.
    /// </summary>
    /// <param name="dialog">Dialog.</param>
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
    /// <summary>
    /// Nexts the dialog (next sentence)
    /// </summary>
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
    /// <summary>
    /// End the of dialog.
    /// </summary>
    void endOfDialog()
    {
        characterName = "";
        Debug.Log("End of dialog!");
    }
}
