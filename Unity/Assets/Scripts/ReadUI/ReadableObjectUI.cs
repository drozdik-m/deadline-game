using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadableObjectUI : MonoBehaviour
{
    public Text ContinueText;

    private float _delay;
    private float _fadingDuration;
    private float _alphaColor;

    private void Start()
    {
        SetActive(false);
        _delay = 4.0f;
        _fadingDuration = 2.0f;
        _alphaColor = 0.5f;
    }

    public void Open()
    {
        Color color = ContinueText.color;
        color.a = 0;
        ContinueText.color = color;

        SetActive(true);
        StartCoroutine(ShowTextEnumerator(_delay, _fadingDuration));
    }

    public void Close()
    {
        SetActive(false);
        StopAllCoroutines();
    }

    private void SetActive(bool status)
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(status);
        }
    }

    private IEnumerator ShowTextEnumerator(float duration, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Original color of text
        var color = ContinueText.color;
        // Time, when the method was run
        var startTime = Time.time;
        // Starting text alpha
        float alpha = 0;
        // Fading step (depends on fading duration)
        float step = 1;

        // Chages alpha channel every frame by step
        while (startTime + duration > Time.time)
        {
            step -= (1 / duration) * Time.deltaTime;
            color.a = Mathf.Lerp(_alphaColor, alpha, step);
            ContinueText.color = color;

            yield return null;
        }
    }
}
