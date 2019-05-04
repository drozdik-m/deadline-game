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
    private string parentGameObjectName;
    private List<OpenableEditor<TEditor>> subEditors;

    private GameObject findOrCreateParentGameObject(P target)
    {
        Type typeOfTarget = target.GetType();
        string parentGameObjectFullName = "_" + typeOfTarget.ToString() + "_" + parentGameObjectName;

        if (GameObjectManager.TryFindChild(target.gameObject,
                                           parentGameObjectFullName,
                                           out GameObject foundParent))
        {
            return foundParent;
        }
        else
            return GameObjectManager.Add(target.gameObject, parentGameObjectFullName);
    }

    private void CleanSubEditors()
    {
        if (subEditors.Count < 1) return;

        foreach (OpenableEditor<TEditor> editor in subEditors)
            Editor.DestroyImmediate(editor.editor);

        subEditors = new List<OpenableEditor<TEditor>>();
    }

    public ArrayEditor(string parentGameObjectName)
    {
        this.parentGameObjectName = parentGameObjectName;
        subEditors = new List<OpenableEditor<TEditor>>();
    }

    public T[] Use(P target)
    {
        // get or create existing items
        GameObject parentGameObject = findOrCreateParentGameObject(target);
        GameObject[] existingItems = GameObjectManager.GetChildren(parentGameObject);

        foreach (GameObject item in existingItems)
        {
            ArrayItem arrItem = new ArrayItem(item.name);
            subEditors.Add(new OpenableEditor<TEditor>(false, Editor.CreateEditor(item.GetComponent<T>()) as TEditor)); 
        }

        foreach (var openableEditor in subEditors)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal();
            openableEditor.shown = EditorGUILayout.Foldout(openableEditor.shown,
                                                           openableEditor.editor.GetFoldoutLabel());

            if (openableEditor.shown)
                openableEditor.editor.OnCustomInspectorGUI();

            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
        }

        return null;
    }

    
}

public class OpenableEditor<TEditor>
{
    public bool shown;
    public TEditor editor;

    public OpenableEditor(bool shown, TEditor editor)
    {
        this.shown = shown;
        this.editor = editor;
    }
}


