using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDialog : MonoBehaviour
{
    [HideInInspector]
    public bool TrueState { get; private set; }

    [HideInInspector]
    public bool FalseState { get; private set; }

    public void SetTrueState()
    {
        TrueState = true;
    }

    public void SetFalseState()
    {
        FalseState = true;
    }
}
