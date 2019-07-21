using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadableObjectUI : MonoBehaviour
{
    private void Start()
    {
        SetActive(false);
    }

    public void Open()
    {
        SetActive(true);
    }

    public void Close()
    {
        SetActive(false);
    }

    private void SetActive(bool status)
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(status);
        }
    }
}
