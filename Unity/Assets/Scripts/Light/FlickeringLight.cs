using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public new Light light;

    /// <summary>
    /// Bottom limit of waiting time
    /// </summary>
    public float minWaitTime;

    /// <summary>
    /// Upper limit of waiting time
    /// </summary>
    public float maxWaitTIme;

    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(Flashing());
    }

    /// <summary>
    /// Change state of the light after time
    /// </summary>
    IEnumerator Flashing()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTIme));
            light.enabled =! light.enabled;
        }
    }
}
