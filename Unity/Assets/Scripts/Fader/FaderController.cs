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
            FadeIn ();
        }
    }

    public void FadeIn(float duration = 3, Color? color = null)
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
        float t = 0;
        while (startTime + duration > Time.time)
        {
            t += (1 / duration ) * Time.deltaTime;
            color.a = Mathf.Lerp (alpha, 0, t);
            fadePanel.color = color;

            yield return null;

        }
        fadePanel.gameObject.SetActive (false);

    }
}
