using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ArrayEditor<T, TEditor>
    where T : MonoBehaviour
    where TEditor : ArrayItemEditor<T>
{
    public static void OnCustomInspectorGUI()
    {
        Debug.Log("On inspector GUI in Array Editor");

        
    }
}