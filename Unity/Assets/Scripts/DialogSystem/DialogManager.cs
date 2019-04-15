﻿using System.Collections;
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
    private Queue<SentenceWrapper> sentences;
    /// <summary>
    /// Dialog status.
    /// </summary>
    private bool isActive;
    /// <summary>
    /// Current dialog bubble target.
    /// </summary>
    public BubbleSpawner spawner;
    private DialogType currentType;

    void Start()
    {
   
        sentences = new Queue<SentenceWrapper>();
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
            sentences.Enqueue(new SentenceWrapper(target.transform, sentence, DialogType.Self));
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
            sentences.Enqueue(new SentenceWrapper(currentTarget.transform, structuredSentence.sentence, DialogType.Twin));
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

        var sentenceWrapper = sentences.Dequeue();
        // Set the current target
        currentType = sentenceWrapper.Type;
        HandlePlayerMovement();

        Transform currentTarget = sentenceWrapper.Position;
        bool isDynamic = sentenceWrapper.Type == DialogType.Self ? true : false;
        spawner.Spawn(ref currentTarget, sentenceWrapper.Sentence, DialogDelay, isDynamic);

    }
    /// <summary>
    /// End the of dialog.
    /// </summary>
    private void endOfDialog()
    {
        isActive = false;
        setPlayerMovement(false);
        CancelInvoke();
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

    private void HandlePlayerMovement()
    {
        if (currentType == DialogType.Self)
        {
            setPlayerMovement(false);
        }
        else
        {
            setPlayerMovement(true);
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
