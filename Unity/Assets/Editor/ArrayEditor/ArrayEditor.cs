﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Threading;
using System.Linq;

/// <summary>
/// 
/// </summary>
/// <typeparam name="P">Wrapper of items type</typeparam>
/// <typeparam name="T">Item type</typeparam>
/// <typeparam name="TEditor">Item editor type</typeparam>
public class ArrayEditor<P, T, TEditor>
    where P : MonoBehaviour
    where T : MonoBehaviour
    where TEditor : Editor, IArrayItemEditor
{
    private string lastMessage = "";
    private GUIStyle lastMessageStyle = GUIStyle.none;

    private P target;
    private EditorMessageBox editorMessageBox;

    private string parentGameObjectName;
    private List<OpenableEditor<TEditor>> subEditors;

    private float buttonWidth = 30f;
    private readonly float verticalSpacing = EditorGUIUtility.standardVerticalSpacing;
    private const float controlSpacing = 5f;
    private const float dropAreaHeight = 50f;

    private Type[] arrayItemTypes;
    private string[] arrayItemTypeNames;
    private int selectedIndex;

    private string userItemName = "";

    private void changeLastMessage(string msg, GUIStyle style)
    {
        lastMessage = msg;
        lastMessageStyle = style;
    }

    private void DestroySubEditors()
    {
        if (subEditors != null)
            foreach (var editor in subEditors)
                Editor.DestroyImmediate(((OpenableEditor<TEditor>)editor).editor);

        subEditors = new List<OpenableEditor<TEditor>>();
    }

    private void LoadEditorsFromHierarchy()
    {
        // get or create existing items
        GameObject parentGameObject = findOrCreateParentGameObject(target);
        GameObject[] existingItems = GameObjectManager.GetChildren(parentGameObject);

        foreach (GameObject item in existingItems)
        {
            subEditors.Add(new OpenableEditor<TEditor>(false, Editor.CreateEditor(item.GetComponent<T>()) as TEditor, new ArrayItem(item.name)));
        }
            

        subEditors = subEditors.OrderBy(s => s.arrayItem.positions.First()).ToList();
    }

    private void InitSubEditors()
    {
        foreach (var openableEditor in subEditors)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;

            EditorGUILayout.BeginHorizontal();

            openableEditor.shown = EditorGUILayout.Foldout(openableEditor.shown, openableEditor.editor.GetFoldoutLabel() + " (" + openableEditor.arrayItem.userName + ")");

            if (GUILayout.Button('\u25B2'.ToString(), GUILayout.Width(buttonWidth)))
                UpArrayItem(openableEditor);

            if (GUILayout.Button('\u25BC'.ToString(), GUILayout.Width(buttonWidth)))
                DownArrayItem(openableEditor);

            if (GUILayout.Button("-", GUILayout.Width(buttonWidth)))
                RemoveArrayItem(openableEditor);

            EditorGUILayout.EndHorizontal();

            if (openableEditor.shown)
                openableEditor.editor.OnInspectorGUI();

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }


        AddItemGUI();
    }

    private void RefreshEditors()
    {
        // destroy editors we already have
        DestroySubEditors();

        // load all editors from hierarchy
        LoadEditorsFromHierarchy();

        // init all editors
        InitSubEditors();
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

    private void AddItemGUI()
    {
        EditorGUILayout.Space();

        GUILayout.BeginVertical(EditorStyles.helpBox);

        GUIStyle headerStyle = new GUIStyle();
        headerStyle.fontSize = 13;
        headerStyle.normal.textColor = Color.white;
        GUILayout.Label("Add item", headerStyle);

        EditorGUILayout.LabelField("choose item type & type name of the item");

        GUILayout.BeginHorizontal();
        selectedIndex = EditorGUILayout.Popup(selectedIndex, arrayItemTypeNames);
        userItemName = GUILayout.TextField(userItemName);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Add"))
        {
            AddArrayItem(selectedIndex, userItemName);
            userItemName = "";
        }
            

        GUILayout.EndVertical();
    }

    private int GetLastArrayItemPosition()
    {
        int lastPosition = 1;

        foreach (var openableEditor in subEditors)
        {
            int maxPos = openableEditor.arrayItem.positions.Max();
            if (maxPos > lastPosition)
                lastPosition = maxPos;
        }
            
        return lastPosition;
    }

    private void AddArrayItem(int selectedIndex, string userItemName)
    {
        if (selectedIndex < 0 || selectedIndex >= arrayItemTypes.Length)
            throw new EditorException("Selected type is not valid");
        if (string.IsNullOrWhiteSpace(userItemName))
        {
            Debug.LogWarning("User item name must be filled");
            return;
        }

        Type selectedType = arrayItemTypes[selectedIndex];

        int newItemPosition = GetLastArrayItemPosition() + 1;
        ArrayItem newArrayItem = new ArrayItem(userItemName, new List<int> { newItemPosition }, selectedType);
        
        GameObject currParent = findOrCreateParentGameObject(target);

        GameObject addedGameObject = GameObjectManager.Add(currParent, newArrayItem.wholeName);
        addedGameObject.AddComponent(selectedType);

        changeLastMessage("Item added", DefaultEditor<MonoBehaviour>.SuccessStyle);
        RefreshEditors();
    }

    private void RemoveArrayItem(OpenableEditor<TEditor> openableEditor)
    {
        if (!GameObjectManager.TryFindChild(findOrCreateParentGameObject(target), openableEditor.arrayItem.wholeName, out GameObject gameObjectToRemove))
            throw new EditorException("Parent of child you are trying to remove was not found");
        GameObjectManager.Remove(gameObjectToRemove);
        RefreshEditors();
    }

    private void UpArrayItem(OpenableEditor<TEditor> openableEditor)
    {
        int minPos = subEditors.Min(s => s.arrayItem.positions.Min());
        if (openableEditor.arrayItem.positions.Min() == minPos)
        {
            return;
        }

        // get the previous item
        var allOtherSubEditors = subEditors.Where(s => s != openableEditor).ToList();
        OpenableEditor<TEditor> firstBefore = subEditors.Where(s => s != openableEditor /* Michalka je bohyně ♥ */ && s.arrayItem.positions.First() == subEditors.Min(se => se.arrayItem.positions.First())).First();

        foreach (var otherEditor in allOtherSubEditors)
        {
            int currPos = otherEditor.arrayItem.positions.First();
            if (currPos > firstBefore.arrayItem.positions.First() && currPos < openableEditor.arrayItem.positions.First())
            {
                firstBefore = otherEditor;
            }
        }

        ArrayItem newCurrentToUp = new ArrayItem(openableEditor.arrayItem.userName, new List<int> { firstBefore.arrayItem.positions.First() }, openableEditor.arrayItem.type);
        ArrayItem newToDown = new ArrayItem(firstBefore.arrayItem.userName, new List<int> { openableEditor.arrayItem.positions.First() }, firstBefore.arrayItem.type);

        // change up
        if (!GameObjectManager.TryFindChild(findOrCreateParentGameObject(target), openableEditor.arrayItem.wholeName, out GameObject foundUp))
            throw new EditorException("Specified game object to move was not found");
        foundUp.name = newCurrentToUp.wholeName;

        // change down
        if (!GameObjectManager.TryFindChild(findOrCreateParentGameObject(target), firstBefore.arrayItem.wholeName, out GameObject foundDown))
            throw new EditorException("Specified game object to move was not found");
        foundDown.name = newToDown.wholeName;

        RefreshEditors();
    }

    private void DownArrayItem(OpenableEditor<TEditor> openableEditor)
    {
        int maxPos = subEditors.Max(s => s.arrayItem.positions.Max());
        if (openableEditor.arrayItem.positions.Max() == maxPos)
        {
            return;
        }

        // get the next item
        var allOtherSubEditors = subEditors.Where(s => s != openableEditor).ToList();
        OpenableEditor<TEditor> firstAfter = subEditors.Where(s => s != openableEditor && s.arrayItem.positions.First() == subEditors.Max(a => a.arrayItem.positions.First())).First();

        foreach (var otherEditor in allOtherSubEditors)
        {
            int currPos = otherEditor.arrayItem.positions.First();
            if (currPos < firstAfter.arrayItem.positions.First() && currPos > openableEditor.arrayItem.positions.First())
            {
                firstAfter = otherEditor;
            }
        }

        ArrayItem newCurrentToDown = new ArrayItem(openableEditor.arrayItem.userName, new List<int> { firstAfter.arrayItem.positions.First() }, openableEditor.arrayItem.type);
        ArrayItem newToUp = new ArrayItem(firstAfter.arrayItem.userName, new List<int> { openableEditor.arrayItem.positions.First() }, firstAfter.arrayItem.type);

        // change up
        if (!GameObjectManager.TryFindChild(findOrCreateParentGameObject(target), openableEditor.arrayItem.wholeName, out GameObject foundDown))
            throw new EditorException("Specified game object to move was not found");
        foundDown.name = newCurrentToDown.wholeName;

        // change down
        if (!GameObjectManager.TryFindChild(findOrCreateParentGameObject(target), firstAfter.arrayItem.wholeName, out GameObject foundUp))
            throw new EditorException("Specified game object to move was not found");
        foundUp.name = newToUp.wholeName;

        RefreshEditors();
    }

    public ArrayEditor(string parentGameObjectName, EditorMessageBox editorMessageBox = null)
    {
        this.parentGameObjectName = parentGameObjectName;
        this.editorMessageBox = editorMessageBox;

        

        subEditors = new List<OpenableEditor<TEditor>>();
        SetItemNamesArray();
    }

    public T[] Use(P sendTarget)
    {
        this.target = sendTarget; 

        if (subEditors != null && subEditors.Count > 0)
            InitSubEditors();
        else
            RefreshEditors();

        if (!string.IsNullOrWhiteSpace(lastMessage))
            editorMessageBox.AddMessage(lastMessage, lastMessageStyle);

        T[] newArr = new T[subEditors.Count];

        for (int i = 0; i < subEditors.Count; i++)
        {
            if (!GameObjectManager.TryFindChild(findOrCreateParentGameObject(target), subEditors[i].arrayItem.wholeName, out GameObject foundGameObject))
                throw new EditorException("Child was not found");

            Type currType = subEditors[i].arrayItem.type;
            newArr[i] = foundGameObject.GetComponent(currType) as T;
        }

        return newArr;
    }
}

public class OpenableEditor<TEditor>
{
    public bool shown;
    public TEditor editor;
    public ArrayItem arrayItem;

    public OpenableEditor(bool shown, TEditor editor, ArrayItem arrayItem)
    {
        this.shown = shown;
        this.editor = editor;
        this.arrayItem = arrayItem;
    }
}


