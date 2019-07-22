using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class displays UI of readable objects 
/// </summary>
public class ReadableObjectUI : MonoBehaviour
{
    /// <summary>
    /// Text, that shown at the bottom of screen
    /// </summary>
    public Text ContinueText;

    /// <summary>
    /// Delay between open readable object and showing text
    /// </summary>
    private float _delay;

    /// <summary>
    /// Duration of showing text 
    /// </summary>
    private float _showingDuration;

    /// <summary>
    /// Final alpha color of the text
    /// </summary>
    private float _finalAlphaColor;

    private void Start()
    {
        SetActive(false);
        _delay = 4.0f;
        _showingDuration = 2.0f;
        _finalAlphaColor = 0.5f;
    }

    /// <summary>
    /// Opens UI of readable object
    /// </summary>
    public void Open()
    {
        Color color = ContinueText.color;
        color.a = 0;
        ContinueText.color = color;

        SetActive(true);
        StartCoroutine(ShowTextEnumerator(_delay, _showingDuration));
    }

    /// <summary>
    /// Closes UI of readable object
    /// </summary>
    public void Close()
    {
        SetActive(false);
        StopAllCoroutines();
    }

    /// <summary>
    /// Sets active of UI
    /// </summary>
    /// <param name="status">Status of UI. Shown, when value is true.</param>
    private void SetActive(bool status)
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(status);
        }
    }

    /// <summary>
    /// Shows text after for a duration
    /// </summary>
    /// <param name="duration">Duration of showing text</param>
    /// <param name="delay">Delay until showing text</param>
    /// <returns></returns>
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
            color.a = Mathf.Lerp(_finalAlphaColor, alpha, step);
            ContinueText.color = color;

            yield return null;
        }
    }
}
