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
    public bool fadeInOut;

    public bool isFading;

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
        } else if (fadeInOut)
        {
            fadeInOut = false;
            FadeInOut ();
        }
    }

    /// <summary>
    /// Function is fading the screen, waiting some time and showing the screen back
    /// </summary>
    /// <param name="durationIn">Screen fading duration in seconds. Default value is 3 seconds.</param>
    /// <param name="colorIn">Panel's Image fading animation color. Default value is black.</param>
    /// <param name="durationOut">Screen showing duration in seconds. Default value is 3 seconds.</param>
    /// <param name="colorOut">Panel's Image showing animation color. Default value is black.</param>
    /// <param name="fadeDelay">Delay between fading and showing back.</param>
    public void FadeInOut(float durationIn = 3,  Color? colorIn = null,
                          float durationOut = 3, Color? colorOut = null, float fadeDelay = 5)
    {
        if (colorIn == null)
            colorIn = black;

        if (colorOut == null)
            colorOut = black;

        Color animColorIn = (Color)colorIn;
        Color animColorOut = (Color)colorIn;

        fadePanel.color = animColorIn;

        coroutine = FadeInOutCoroutine (durationIn, animColorIn, durationOut, animColorOut, fadeDelay);
        StartCoroutine (coroutine);
    }

    /// <summary>
    /// Supporting function for FadeInOut, which produces the fading and the showing of the screen.
    /// </summary>
    private IEnumerator FadeInOutCoroutine(float durationIn,  Color colorIn,
                                           float durationOut, Color colorOut, float fadeDelay)
    {
        FadeIn (durationIn, colorIn);
        yield return new WaitForSeconds (fadeDelay + durationIn);
        FadeOut (durationOut, colorOut);
    }

    /// <summary>
    /// Function is showing the screen in time
    /// </summary>
    /// <param name="duration">Screen showing duration in seconds. Default value is 3 seconds.</param>
    /// <param name="color">Panel's Image animation color. Default value is black.</param>
    public void FadeOut(float duration = 3, Color? color = null)
    {
        if (color == null)
            color = black;

        Color animColor = (Color)color;
        animColor.a = fadePanel.color.a;
        fadePanel.color = animColor;

        // Stop actual coroutine
        if (coroutine != null)
            StopCoroutine (coroutine);

        coroutine = FadeOutCoroutine (duration, animColor);
        StartCoroutine (coroutine);

    }

    /// <summary>
    /// Function is fading the screen in time
    /// </summary>
    /// <param name="duration">Screen fading duration in seconds. Default value is 3 seconds.</param>
    /// <param name="color">Panel's Image animation color. Default value is black.</param>
    public void FadeIn(float duration = 3, Color? color = null )
    {
        if (color == null)
            color = black;

        Color animColor = (Color)color;
        fadePanel.color = animColor;

        coroutine = FadeInCoroutine (duration, animColor);
        StartCoroutine (coroutine);
    }

    /// <summary>
    /// Supporting function for FadeIn, which produces the fading of screen.
    /// </summary>
    private IEnumerator FadeInCoroutine(float duration, Color color )
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
    /// Supporting function for FadeOut, which produces the appearance of screen.
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

   
