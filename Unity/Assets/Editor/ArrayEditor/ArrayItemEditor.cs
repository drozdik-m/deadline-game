using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
    

public abstract class ArrayItemEditor<T> : DefaultEditor<T>
    where T : MonoBehaviour
{
    public override void OnCustomInspectorGUI()
    {
        EditorGUILayout.LabelField("This is array item editor");
        OnArrayItemInspectorGUI();
    }

    public abstract void OnArrayItemInspectorGUI();
}
