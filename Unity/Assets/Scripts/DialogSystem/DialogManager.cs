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
    /// Refference to player One (main player).
    /// </summary>
    public GameObject playerOne;
    /// <summary>
    /// Refference to player Two (usually npc).
    /// </summary>
    public GameObject playerTwo;
    /// <summary>
    /// Refference to dialog canvas.
    /// </summary>
    public GameObject dialogCanvas;
    /// <summary>
    /// Refference to dialog text label.
    /// </summary>
    public TextMeshProUGUI textLabel;
    /// <summary>
    /// Horizontal offset of the dialog bubble.
    /// </summary>
    [Range(-2, 2)]
    public float offsetHorizontal;
    /// <summary>
    /// Vertical offset of the dialog bubble.
    /// </summary>
    [Range(-2, 2)]
    public float offsetVertical;
    /// <summary>
    /// Delay between each sentence (in seconds).
    /// </summary>
    [Range(1,8)]
    public float dialogDelay;
    /// <summary>
    /// Queue of sentences (dialog).
    /// </summary>
    private Queue<KeyValuePair<int,string>> sentences;
    /// <summary>
    /// Dialog status.
    /// </summary>
    private bool isActive;
    /// <summary>
    /// Current dialog bubble target.
    /// </summary>
    private GameObject currentTarget;
    private string currentSentence;

    void Start()
    {
        sentences = new Queue<KeyValuePair<int,string>>();
        isActive = false;

    }

    private void Update()
    {
        dialogCanvas.SetActive(isActive);
        if (isActive)
        {
            handlePosition();
            textLabel.SetText(currentSentence);
        }
    }

    /// <summary>
    /// Starts the dialog.
    /// </summary>
    /// <param name="dialog">SelfTalkDialog.</param>
    public void startDialog(SelfTalkDialog dialog)
    {
        if (playerOne == null)
        {
            Debug.Log("PlayerOne not assigned!");
            return;
        }
        if (isActive)
        {
            Debug.Log("Another dialog is in progress!");
            return;
        }

        sentences.Clear();
        currentTarget = dialog.characterId == 0 ? playerOne : playerTwo;

        foreach (var sentence in dialog.sentences)
        {
            sentences.Enqueue(new KeyValuePair<int, string>(dialog.characterId, sentence));
        }
         // Repeats calling nextSentece() after a user defined delay
        InvokeRepeating("nextSentence", 0, dialogDelay);


    }
    /// <summary>
    /// Starts the dialog.
    /// </summary>
    /// <param name="dialog">TwinTalkDialog.</param>
    public void startDialog(TwinTalkDialog dialog)
    {
        if (playerTwo == null)
        {
            Debug.Log("PlayerTwo not assigned!");
            return;
        }
        if (isActive)
        {
            Debug.Log("Another dialog is in progress!");
            return;
        }

        sentences.Clear();

        foreach (var structuredSentence in dialog.structuredSentences)
        {
            sentences.Enqueue(new KeyValuePair<int, string>(structuredSentence.characterId, structuredSentence.sentence));
        }
        // Repeats calling nextSentece() after a user defined delay
        InvokeRepeating("nextSentence", 0, dialogDelay);
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

        var sentenceStruct = sentences.Dequeue();
        // Set the current target
        currentTarget = sentenceStruct.Key == 0 ? playerOne : playerTwo;
        currentSentence = sentenceStruct.Value;

        isActive = true;

    }
    /// <summary>
    /// End the of dialog.
    /// </summary>
    void endOfDialog()
    {
        Debug.Log("End of dialog!");
        isActive = false;
        CancelInvoke();
    }
    /// <summary>
    /// Handles the position of the dialog bubble according to target.
    /// </summary>
    void handlePosition()
    {
        if (isActive)
        {
            Vector3 offset = new Vector3(offsetHorizontal, offsetVertical, -offsetHorizontal);
            dialogCanvas.transform.position = currentTarget.transform.position + offset;
        }
    }

}
