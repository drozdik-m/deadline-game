using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Dialog manager.
/// </summary>
public class DialogManager : MonoBehaviour
{
    public GameObject dialogCanvas;
    public bool isActive;
    public GameObject headCheck;
    public TextMeshProUGUI textLabel;

    [Range(-2, 2)]
    public float offsetHorizontal;

    [Range(-2, 2)]
    public float offsetVertical;
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
        isActive = false;

    }

    private void Update()
    {
        dialogCanvas.SetActive(isActive);
        Vector3 offset = new Vector3(offsetHorizontal, offsetVertical, -offsetHorizontal);
        dialogCanvas.transform.position = headCheck.transform.position + offset;

        if (Input.GetKeyDown("return") && isActive)
            nextSentence();

    }

    /// <summary>
    /// Starts the dialog.
    /// </summary>
    /// <param name="dialog">Dialog.</param>
    public void startDialog(SelfTalkDialog dialog)
    {
        sentences.Clear();
        isActive = true;

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
        textLabel.SetText(sentenceToDisplay);
    }
    /// <summary>
    /// End the of dialog.
    /// </summary>
    void endOfDialog()
    {
        characterName = "";
        Debug.Log("End of dialog!");
        isActive = false;
    }
}
