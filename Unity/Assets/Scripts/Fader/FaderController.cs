using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Class provide screen fading and screen appearance
/// </summary>
public class FaderController : MonoBehaviour
{
    public bool fadeOut;
    public bool fadeIn;

    /// <summary>
    /// Default color for panel
    /// </summary>
    private Color black = Color.black;

    /// <summary>
    /// Panel's Image that will be fading
    /// </summary>
    private Image fadePanel;

    /// <summary>
    /// Contain actual coroutine function
    /// </summary>
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        fadePanel = GetComponent<Image> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut)
        {
            fadeOut = false;
            FadeOut ();
        } else if (fadeIn)
        {
            fadeIn = false;
            FadeIn ();
        }
    }

    /// <summary>
    /// Function is showing the screen in time
    /// </summary>
    /// <param name="duration">Screen showing duration in seconds. Default value is 3 seconds.</param>
    /// <param name="color">Panel's Image resulting color. Default value is black</param>
    public void FadeOut( float duration = 3, Color? color = null)
    {
        if (color == null)
            color = black;

        Color changeColor = (Color)color;
        fadePanel.color = changeColor;

        if (coroutine != null)
            StopCoroutine (coroutine);
        coroutine = FadeOutCoroutine (duration, changeColor);
        StartCoroutine (coroutine);

    }

    public void FadeIn(float duration = 3, Color? color = null )
    {
        if (color == null)
            color = black;

        Color changeColor = (Color)color;
        fadePanel.color = changeColor;

        coroutine = FadeInCoroutine (duration, changeColor);
        StartCoroutine (coroutine);
    }

    private IEnumerator FadeInCoroutine( float duration, Color color )
    {
        // Time, when the script was run
        float startTime = Time.time;
        // Starting image alpha
        float alpha = color.a;
        // Fading step (depends on fading duration)
        float step = color.a;

        // Chages alpha channel every frame by step
        while (startTime + duration > Time.time)
        {
            step -= ( 1 / duration ) * Time.deltaTime;
            color.a = Mathf.Lerp (alpha, 0, step);
            fadePanel.color = color;

            yield return null;
        }
    }

    /// <summary>
    /// Supporting function for FadeOut, which calculetes the appearance of screen.
    /// </summary>
    private IEnumerator FadeOutCoroutine( float duration, Color color )
    {
        // Time, when the script was run
        float startTime = Time.time;
        // Starting image alpha
        float alpha = color.a;
        // Fading step (depends on fading duration)
        float step = 0;

        // Chages alpha channel every frame by step
        while (startTime + duration > Time.time)
        {
            step += ( 1 / duration ) * Time.deltaTime;
            color.a = Mathf.Lerp (alpha, 0, step);
            fadePanel.color = color;

            yield return null;
        }
    }
}

   
