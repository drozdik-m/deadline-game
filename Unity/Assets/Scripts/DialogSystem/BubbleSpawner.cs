using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    private Queue<GameObject> instances;

    // Start is called before the first frame update
    void Start()
    {
        instances = new Queue<GameObject>();
        if (!BubblePrefab)
            Debug.Log("Missing DialogBubble prefab in DialogBubble script!");
    }
    /// <summary>
    /// Spawn the bubble at specified position, text and delay.
    /// </summary>
    /// <param name="position">Position.</param>
    /// <param name="text">Text.</param>
    /// <param name="delay">Delay.</param>
    public void Spawn(Transform position,string text,float delay)
    {
        position.rotation = Quaternion.Euler(0, Rotation, 0);
        GameObject bubbleInstance = (GameObject)Instantiate(BubblePrefab,
        position.transform.position, position.transform.rotation);


        TextMeshProUGUI textLabel = bubbleInstance.GetComponentInChildren<TextMeshProUGUI>();
        textLabel.SetText(text);

        instances.Enqueue(bubbleInstance);
        // Wait for n (delay) seconds and destroy the object
        StartCoroutine(WaitFor(delay));

    }

    /// <summary>
    /// Waits for certain delay and destroy bubble object afterwards.
    /// </summary>
    /// <returns>The for.</returns>
    /// <param name="delay">Delay.</param>
    IEnumerator WaitFor(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("destroy");
        Destroy(instances.Dequeue());
    }
}
