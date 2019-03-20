using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class testFader : MonoBehaviour
{
    FaderController fader;

    Color color = Color.black;

    // Start is called before the first frame update
    void Start()
    {
        fader = GetComponent<FaderController> ();
    }

    public void FaidInTest()
    {
        fader.FadeIn (3, color);
    }

    public void FaidOutTest()
    {
        fader.FadeOut (3, color);
    }

    public void FaidInOutTest()
    {
        fader.FadeInOut (3, color, 3, color, 5);
    }

    public void isFadingTest()
    {
        Debug.Log ("Is Fading " + fader.isFading ());
    }

    public void ChangeColor()
    {
        color = (color == Color.black ? Color.green : Color.black);
    }

}
