using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class displays UI of readable objects 
/// </summary>
public class ReadableObjectUI : MonoBehaviour
{
    /// <summary>
    /// Event when UI of readable object is opened
    /// </summary>
    public event ReadableObjectUIChangeEventHandler OnOpen;

    /// <summary>
    /// Event when UI of readable object is closed
    /// </summary>
    public event ReadableObjectUIChangeEventHandler OnClose;

    /// <summary>
    /// Text, that shows at the bottom of screen
    /// </summary>
    static private Text ContinueText;

    /// <summary>
    /// Button, that closes UI of readable object
    /// </summary>
    static private Button QuitButton;

    /// <summary>
    /// Delay between open readable object and showing text
    /// </summary>
    private float _delay = 3.0f;

    /// <summary>
    /// Duration of showing text 
    /// </summary>
    private float _showingDuration = 5.0f;

    /// <summary>
    /// Final alpha color of the text
    /// </summary>
    private float _finalAlphaColor = 0.5f;

    private void Start()
    {
        if (!QuitButton)
            QuitButton = GameObject.Find("BackgroundButton").GetComponent<Button>();
        if (!ContinueText)
            ContinueText = QuitButton.GetComponentInChildren<Text>();

        SetActive(false);
    }

    /// <summary>
    /// Sets values of showing duration and delay of Continue text parametr
    /// </summary>
    /// <param name="duration">Duration of showing text</param>
    /// <param name="delay">Delay until showing text</param>
    public void SetTimingOfContinueText(float duration, float delay)
    {
        _delay = delay;
        _showingDuration = duration;
    }

    /// <summary>
    /// Checks if readable object is open
    /// </summary>
    /// <returns>Status of readable object. True, if it's active</returns>
    public bool IsOpen()
    {
        return gameObject.activeSelf;
    }

    /// <summary>
    /// Opens UI of readable object
    /// </summary>
    public void Open()
    {
        // Set color of Continue text
        Color color = ContinueText.color;
        color.a = 0;
        ContinueText.color = color;

        // Activate readable object
        SetActive(true);
        QuitButton.onClick.AddListener(Close);

        // Event
        OnOpen?.Invoke(this, new ReadableObjectUIArgs(true));

        StartCoroutine(ShowTextEnumerator(_showingDuration, _delay));
    }

    /// <summary>
    /// Closes UI of readable object
    /// </summary>
    public void Close()
    {
        // Deactivate readable object
        SetActive(false);
        StopAllCoroutines();

        // Event
        OnClose?.Invoke(this, new ReadableObjectUIArgs(false));
    }

    /// <summary>
    /// Sets active of UI
    /// </summary>
    /// <param name="status">Status of UI. Shown, when value is true.</param>
    private void SetActive(bool status)
    {
        gameObject.SetActive(status);
        QuitButton.gameObject.SetActive(status);
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

        // Changes alpha channel every frame by step
        while (startTime + duration > Time.time)
        {
            step -= (1 / duration) * Time.deltaTime;
            color.a = Mathf.Lerp(_finalAlphaColor, alpha, step);
            ContinueText.color = color;

            yield return null;
        }
    }
}

/// <summary>
/// Delegate for UI of readable object change events
/// </summary>
/// <param name="caller">Readable object that called events</param>
/// <param name="e">Arguments</param>
public delegate void ReadableObjectUIChangeEventHandler(ReadableObjectUI caller, ReadableObjectUIArgs e);
