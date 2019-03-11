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
    /// <summary>
    /// Refference to the dialog bubble canvas.
    /// </summary>
    public GameObject dialogCanvas;
    /// <summary>
    /// Refference to the player head point.
    /// </summary>
    public GameObject playerHead;
    /// <summary>
    /// Refference to the dialog textLabel.
    /// </summary>
    public TextMeshProUGUI textLabel;
    /// <summary>
    /// Status of the dialog.
    /// </summary>
    private bool isActive;
    /// <summary>
    /// The horizontal offset.
    /// </summary>
    [Range(-2, 2)]
    public float offsetHorizontal;
    /// <summary>
    /// The vertical offset.
    /// </summary>
    [Range(-2, 2)]
    public float offsetVertical;
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
        // Offset the bubble anchor
        Vector3 offset = new Vector3(offsetHorizontal, offsetVertical, -offsetHorizontal);
        // Stick the bubble to playerHead point
        dialogCanvas.transform.position = playerHead.transform.position + offset;

        // DELETE
        if (Input.GetKeyDown("return") && isActive)
            NextSentence();
        // DELETE

    }

    /// <summary>
    /// Starts the dialog.
    /// </summary>
    /// <param name="dialog">Dialog.</param>
    public void StartDialog(SelfTalkDialog dialog)
    {
        sentences.Clear();
        isActive = true;

        foreach (var sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        NextSentence();
    }
    /// <summary>
    /// Nexts the dialog (next sentence)
    /// </summary>
    public void NextSentence()
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
        isActive = false;
    }
}
