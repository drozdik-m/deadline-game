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
    private Queue<SentenceWrapper> sentences;
    /// <summary>
    /// Dialog status.
    /// </summary>
    private bool isActive;
    /// <summary>
    /// Current dialog bubble target.
    /// </summary>
    public BubbleSpawner spawner;
    /// <summary>
    /// The type of the current.
    /// </summary>
    private DialogType currentType;
    /// <summary>
    /// The currently displayed.
    /// </summary>
    private string currentlyDisplayed;
    /// <summary>
    /// The auto delay.
    /// </summary>
    public bool autoDelay;
    /// <summary>
    /// The current delay.
    /// </summary>
    private float currentDelay;
    /// <summary>
    /// The sound effect controller.
    /// </summary>
    private SoundEffectController soundEffectController;

    private DialogManager()
    {
        sentences = new Queue<SentenceWrapper>();
        isActive = false;
    }

    void Start()
    {
        currentlyDisplayed = null;

        soundEffectController = GetComponent<SoundEffectController>();
        if (!soundEffectController)
            Debug.LogError("BubbleSpawner: SoundEffectController not found");


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
            var newSentence = new SentenceWrapper(target.transform, sentence, DialogType.Self);
            if (sentences.Contains(newSentence) || newSentence.Sentence == currentlyDisplayed)
            {
                return;
            }
            sentences.Enqueue(newSentence);
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

            var newSentence = new SentenceWrapper(currentTarget.transform, structuredSentence.sentence, DialogType.Twin);

            if (sentences.Contains(newSentence) || newSentence.Sentence == currentlyDisplayed )
            {
                return;
            }

            sentences.Enqueue(newSentence);
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

        currentlyDisplayed = sentenceWrapper.Sentence;

        // Set the current target
        currentType = sentenceWrapper.Type;
        HandlePlayerMovement();

        Transform currentTarget = sentenceWrapper.Position;
        bool isDynamic = sentenceWrapper.Type == DialogType.Self ? true : false;

        if (autoDelay)
        {
            currentDelay = calculateDelayToLength(sentenceWrapper.Sentence);
            spawner.Spawn(ref currentTarget, sentenceWrapper.Sentence, currentDelay , isDynamic);
        }
        else
        {
            currentDelay = DialogDelay;
            spawner.Spawn(ref currentTarget, sentenceWrapper.Sentence, currentDelay, isDynamic);
        }



    }
    /// <summary>
    /// End the of dialog.
    /// </summary>
    private void endOfDialog()
    {
        currentlyDisplayed = null;
        isActive = false;
        setPlayerMovement(false);
        CancelInvoke();
        currentDelay = 0;
        soundEffectController.StopCurrentSound();
    }

    private float calculateDelayToLength(string sentence)
    {
        int strlen = sentence.Length;
        float calculatedDelay = strlen * 0.15f;
        if (calculatedDelay < 1.5)
            calculatedDelay = 1.5f;

        if (calculatedDelay > 3.5)
        {
            calculatedDelay = 3.5f;
        }
        return calculatedDelay;     

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
            currentDelay = 0;
            isActive = true;
            setPlayerMovement(isBlocking);

            if (!soundEffectController)
                soundEffectController = GetComponent<SoundEffectController>();

            soundEffectController.PlaySound();
            //Debug.Log("Sound (" + (bool)soundEffectController + ")");

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
            yield return new WaitForSeconds(currentDelay);
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
