using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaderController : MonoBehaviour
{
    public bool fadeIn;

    private Color black = Color.black;
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
            FadeIn (10);
        }
    }

    public void FadeIn(int duration = 3, Color? color = null)
    {
        if (color == null)
            color = black;
        Color changeColor = (Color)color;
        fadePanel.color = changeColor;

        Debug.Log ("FadeIn Start");

        StartCoroutine (FadeInCoroutine (duration, changeColor));

    }

    private IEnumerator FadeInCoroutine(float duration, Color color)
    {
        float startTime = Time.time;
        float alpha = color.a;
        while (startTime + duration > Time.time)
        {
            alpha = Mathf.Lerp (alpha, 0, Time.deltaTime / duration );
            color.a = alpha;
            fadePanel.color = color;

            yield return null;

        }
        Debug.Log ("FadeIn End");

    }
}
