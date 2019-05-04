using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
    

public abstract class ArrayItemEditor<T> : DefaultEditor<T>
    where T : MonoBehaviour
{
    public override void OnCustomInspectorGUI()
    {
        OnArrayItemInspectorGUI();
    }

    public abstract void OnArrayItemInspectorGUI();

    public abstract string GetFoldoutLabel();
}
