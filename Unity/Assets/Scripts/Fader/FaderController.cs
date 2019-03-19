using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Class provide screen fading and screen appearance
/// </summary>
public class FaderController : MonoBehaviour
{
    public bool fadeIn;

    /// <summary>
    /// Default color for panel
    /// </summary>
    private Color black = Color.black;

    /// <summary>
    /// Panel's Image that will be fading
    /// </summary>
    private Image fadePanel;

    // Start is called before the first frame update
    void Start()
    {
        fadePanel = GetComponent<Image> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            fadeIn = false;
            FadeIn ();
        }
    }

    /// <summary>
    /// Function is fading the screen in time
    /// </summary>
    /// <param name="duration">Screen fading duration in seconds. Default value is 3 seconds.</param>
    /// <param name="color">Panel's Image starting color. Default value is black</param>
    public void FadeIn(float duration = 3, Color? color = null)
    {
        if (color == null)
            color = black;

        Color changeColor = (Color)color;
        fadePanel.color = changeColor;

        StartCoroutine (FadeInCoroutine (duration, changeColor));

    }

    /// <summary>
    /// Supporting function for FadeIn, which calculetes the fading.
    /// </summary>
    private IEnumerator FadeInCoroutine(float duration, Color color)
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
            step += (1 / duration ) * Time.deltaTime;
            color.a = Mathf.Lerp (alpha, 0, step);
            fadePanel.color = color;

            yield return null;
        }

        // Deactivate panel
        fadePanel.gameObject.SetActive (false);
    }
}
