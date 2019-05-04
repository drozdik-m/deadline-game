using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

/// <summary>
/// 
/// </summary>
/// <typeparam name="P">Wrapper of items type</typeparam>
/// <typeparam name="T">Item type</typeparam>
/// <typeparam name="TEditor">Item editor type</typeparam>
public class ArrayEditor<P, T, TEditor>
    where P : MonoBehaviour
    where T : MonoBehaviour
    where TEditor : ArrayItemEditor<T>
{
    private static GameObject findOrCreateParentGameObject(P target)
    {
        Type typeOfTarget = target.GetType();
        string parentGameObjectName = "_" + typeOfTarget.ToString() + "_";

        if (GameObjectManager.TryFindChild(target.gameObject,
                                           parentGameObjectName,
                                           out GameObject foundParent))
        {
            return foundParent;
        }
        else
            return GameObjectManager.Add(target.gameObject, parentGameObjectName);
    }

    public static T[] CreateArrayEditor(P target)
    {
        Debug.Log("On inspector GUI in Array Editor");

        // get or create existing items
        GameObject parentGameObject = findOrCreateParentGameObject(target);
        GameObject[] existingItems = GameObjectManager.GetChildren(parentGameObject);

        EditorGUI.indentLevel++;
        foreach (GameObject item in existingItems)
        {
            ArrayItem arrItem;
            arrItem = new ArrayItem(item.name);
            DefaultEditor<T> ed = Editor.CreateEditor(item.GetComponent<T>()) as DefaultEditor<T>;
            ed.OnCustomInspectorGUI();
        }
        EditorGUI.indentLevel--;

        return null;
    }

    
}