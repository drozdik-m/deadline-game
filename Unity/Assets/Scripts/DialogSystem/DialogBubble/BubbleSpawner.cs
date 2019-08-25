using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Spawns and controls bubbles
/// </summary>
public class BubbleSpawner : MonoBehaviour
{
    /// <summary>
    /// Refference to the bubble prefab.
    /// </summary>
    public GameObject BubblePrefab;
    /// <summary>
    /// The bubble instance.
    /// </summary>
    public int Rotation = 45;
    private SoundEffectController soundEffectController;


    // Start is called before the first frame update
    void Start()
    {
        if (!BubblePrefab)
            Debug.Log("Missing DialogBubble prefab in DialogBubble script!");

        soundEffectController = GetComponent<SoundEffectController>();
        if (!soundEffectController)
            Debug.LogError("BubbleSpawner: SoundEffectController not found");


    }
    /// <summary>
    /// Spawn the bubble at specified position, text and delay.
    /// </summary>
    /// <param name="position">Position.</param>
    /// <param name="text">Text.</param>
    /// <param name="delay">Delay.</param>
    /// <param name="isDynamic">If set to <c>true</c> is dynamic.</param>
    public void Spawn(ref Transform position,string text,float delay, bool isDynamic)
    {


        // Sound
        soundEffectController.PlaySound();

        position.rotation = Quaternion.Euler(0, Rotation, 0);
        GameObject bubbleInstance = (GameObject)Instantiate(BubblePrefab,
        position.transform.position, position.transform.rotation);


        TextMeshProUGUI textLabel = bubbleInstance.GetComponentInChildren<TextMeshProUGUI>();
        Bubble bubble = bubbleInstance.GetComponentInChildren<Bubble>();
        bubble.delay = delay;
        bubble.isDynamic = isDynamic;
        bubble.SetRef(ref position);
        textLabel.SetText(text);
    

    }

}
