using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScenes_Tests : MonoBehaviour
{
    public void ChangeToScene1()
    {
        GameObject.FindGameObjectWithTag("InvincibleObject").
            GetComponent<ScenesChangeManager>().ChangeScene("ScenesManagerTests_Scene1");
    }

    public void ChangeToScene2()
    {
        GameObject.FindGameObjectWithTag("InvincibleObject").
            GetComponent<ScenesChangeManager>().ChangeScene("ScenesManagerTests_Scene2", Color.red, 5);
    }
}
