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

    private float buttonWidth = 30f;
    private readonly float verticalSpacing = EditorGUIUtility.standardVerticalSpacing;
    private const float controlSpacing = 5f;
    private const float dropAreaHeight = 50f;

    private Type[] arrayItemTypes;
    private string[] arrayItemTypeNames;
    private int selectedIndex;

    private void InitSubEditors()
    {
        foreach (var openableEditor in subEditors)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;

            EditorGUILayout.BeginHorizontal();

            openableEditor.shown = EditorGUILayout.Foldout(openableEditor.shown, openableEditor.editor.GetFoldoutLabel());

            if (GUILayout.Button('\u25B2'.ToString(), GUILayout.Width(buttonWidth)))
            {
                Debug.Log("Up not implemented");
            }

            if (GUILayout.Button('\u25BC'.ToString(), GUILayout.Width(buttonWidth)))
            {
                Debug.Log("Down not implemented");
            }

            if (GUILayout.Button("-", GUILayout.Width(buttonWidth)))
            {
                Debug.Log("Removing not implemented");
            }

            EditorGUILayout.EndHorizontal();

            if (openableEditor.shown)
                openableEditor.editor.OnCustomInspectorGUI();

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }


        TypeSelectionGUI();
    }

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

    private void SetItemNamesArray()
    {
        Type type = typeof(T);

        Type[] allTypes = type.Assembly.GetTypes();

        List<Type> subTypeList = new List<Type>();

        for (int i = 0; i < allTypes.Length; i++)
        {
            if (allTypes[i].IsSubclassOf(type) && !allTypes[i].IsAbstract)
            {
                subTypeList.Add(allTypes[i]);
            }
        }

        arrayItemTypes = subTypeList.ToArray();

        List<string> typeNameList = new List<string>();

        for (int i = 0; i < arrayItemTypes.Length; i++)
        {
            typeNameList.Add(arrayItemTypes[i].Name);
        }

        arrayItemTypeNames = typeNameList.ToArray();
    }

    private void TypeSelectionGUI()
    {
        Debug.Log(arrayItemTypeNames.Length);

        selectedIndex = EditorGUILayout.Popup(selectedIndex, arrayItemTypeNames);

        if (GUILayout.Button("Add"))
        {

            // todo add select item to array
            Debug.Log("Adding not implemented");
        }
    }

    public ArrayEditor(string parentGameObjectName)
    {
        this.parentGameObjectName = parentGameObjectName;
        subEditors = new List<OpenableEditor<TEditor>>();
        SetItemNamesArray();
    }

    public T[] Use(P target)
    {
        if (subEditors.Count > 0)
        {
            InitSubEditors();
            return null;
        }
            
        // get or create existing items
        GameObject parentGameObject = findOrCreateParentGameObject(target);
        GameObject[] existingItems = GameObjectManager.GetChildren(parentGameObject);

        foreach (GameObject item in existingItems)
        {
            ArrayItem arrItem = new ArrayItem(item.name);
            subEditors.Add(new OpenableEditor<TEditor>(false, Editor.CreateEditor(item.GetComponent<T>()) as TEditor)); 
        }

        InitSubEditors();

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


