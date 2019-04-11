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
    /// The dialog delay.
    /// </summary>
    [Range(1, 8)]
    public float DialogDelay = 3f;
    /// <summary>
    /// Queue of sentences (dialog).
    /// </summary>
    private Queue<KeyValuePair<Transform,string>> sentences;
    /// <summary>
    /// Dialog status.
    /// </summary>
    private bool isActive;
    /// <summary>
    /// Current dialog bubble target.
    /// </summary>
    public BubbleSpawner spawner;

    void Start()
    {
        sentences = new Queue<KeyValuePair<Transform,string>>();
        isActive = false;
    }

    /// <summary>
    /// Starts the dialog.
    /// </summary>
    /// <param name="dialog">SelfTalkDialog.</param>
    public void AddDialog(SelfTalkDialog dialog, GameObject target = null )
    {
        // AKA if the target is not passed by paramater, then use the player as target
        if (target == null) 
        {
            target = getPlayerHead();
        }


        foreach (var sentence in dialog.sentences)
        {
            sentences.Enqueue(new KeyValuePair<Transform, string>(target.transform, sentence));
        }

        prepareForStart();

    }
    /// <summary>
    /// Starts the dialog.
    /// </summary>
    /// <param name="dialog">TwinTalkDialog.</param>
    public void AddDialog(TwinTalkDialog dialog, GameObject targetB, GameObject targetA  = null)
    {

        if (targetA == null)
        {
            targetA = getPlayerHead();
        }

        foreach (var structuredSentence in dialog.structuredSentences)
        {
            var currentTarget = structuredSentence.CharacterID == TwinTalkDialog.SentenceStructure.CharacterIdentifier.A  ? targetA : targetB;
            sentences.Enqueue(new KeyValuePair<Transform, string>(currentTarget.transform, structuredSentence.sentence));
        }
        prepareForStart();
    }

    /// <summary>
    /// Nexts the dialog (next sentence)
    /// </summary>
    private void nextSentence()
    {
        if (sentences.Count == 0)
        {
            endOfDialog();
            return;
        }

        var sentenceStruct = sentences.Dequeue();
        // Set the current target
        Transform currentTarget = sentenceStruct.Key;
        spawner.Spawn(currentTarget, sentenceStruct.Value, DialogDelay);

    }
    /// <summary>
    /// End the of dialog.
    /// </summary>
    private void endOfDialog()
    {
        Debug.Log("End of dialog!");
        isActive = false;
        CancelInvoke();
        setPlayerMovement(false);
    }
    /// <summary>
    /// Gets the player head (point of bubble render).
    /// </summary>
    /// <returns>The player head.</returns>
    private GameObject getPlayerHead()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            GameObject headCheck = player.transform.Find("HeadCheck").gameObject;
            if (headCheck)
            {
                return headCheck;
            }
        }
        return null;
    }
    /// <summary>
    /// Prepares for start of the dialog a starts it.
    /// </summary>
    private void prepareForStart(bool isBlocking = true)
    {
        if (!isActive)
        {

            isActive = true;
            setPlayerMovement(isBlocking);
            nextSentence();
            StartCoroutine(Invoker());
        }
    }
    /// <summary>
    /// Invokes each sentence.
    /// </summary>
    /// <returns>The invoker.</returns>
    IEnumerator Invoker()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(DialogDelay);
            nextSentence();
        }
    }


    private void setPlayerMovement(bool isDisabled)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
        
            PlayerMovementController pmc = player.GetComponent<PlayerMovementController>();
            if (pmc)
            { 
            pmc.SetState(isDisabled);
            }
        }
    }
    /// <summary>
    /// Dialog is in progress.
    /// </summary>
    /// <returns><c>true</c>, if in progress was dialoged, <c>false</c> otherwise.</returns>
    public bool DialogInProgress()
    {
        return isActive;
    }



}
