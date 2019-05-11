using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dialog bubble class
/// </summary>
public class Bubble : MonoBehaviour
{
    /// <summary>
    /// The spawn delay for each bubble
    /// </summary>
    public float delay;
    /// <summary>
    /// Bubble is dynamic, moves with target
    /// </summary>
    public bool isDynamic;
    /// <summary>
    /// The position of the bubble
    /// </summary>
    private Transform position;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitFor(delay));
    }

    public void SetRef(ref Transform pos)
    {
        position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDynamic)
        {
            this.transform.position = position.position;
        }
    }

    /// <summary>
    /// Waits for given delay and deletes the object afterwards
    /// </summary>
    /// <returns>The for.</returns>
    /// <param name="del">Del.</param>
    IEnumerator WaitFor(float del)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
