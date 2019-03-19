using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testFader : MonoBehaviour
{
    [TextArea]
    public string Help = "Test script. Shortcuts: W - FadeIn, A - FadeOut, D - FadeInOut";

    FaderController fader;

    // Start is called before the first frame update
    void Start()
    {
        fader = GetComponent<FaderController> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.W))
        {
            fader.FadeIn ();
        }
        else if (Input.GetKeyDown (KeyCode.A))
        {
            fader.FadeOut ();
        }
        else if (Input.GetKeyDown (KeyCode.D))
        {
            fader.FadeInOut ();
        }
    }
}
