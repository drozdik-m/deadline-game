using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for scenes change testing
/// </summary>
public class ChangeScenes_Tests : MonoBehaviour
{
    /// <summary>
    /// Testing method for scenes change
    /// </summary>
    public void ChangeToScene1()
    {
        GameObject.FindGameObjectWithTag("InvincibleObject").
            GetComponent<ScenesChangeManager>().ChangeScene("ScenesManagerTests_Scene1");
    }

    /// <summary>
    /// Testing method for scenes change
    /// </summary>
    public void ChangeToScene2()
    {
        GameObject.FindGameObjectWithTag("InvincibleObject").
            GetComponent<ScenesChangeManager>().ChangeScene("ScenesManagerTests_Scene2", Color.red, 5);
    }
}
