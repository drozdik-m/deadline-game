using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesChangeManager : MonoBehaviour
{

    private void Start()
    {
        
    }

    public void ChangeScene(string newSceneName)
    {
        Initiate.Fade(newSceneName, Color.black, 1);
    }

    public void ChangeScene(string newSceneName, Color color, float multiplier)
    {
        Initiate.Fade(newSceneName, color, multiplier);
    }
}
