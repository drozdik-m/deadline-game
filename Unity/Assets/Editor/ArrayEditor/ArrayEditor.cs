using System.Collections;
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
    where TEditor : ArrayItemEditor<T>
{
    private P target;

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
            subEditors.Add(new OpenableEditor<TEditor>(false, Editor.CreateEditor(item.GetComponent<T>()) as TEditor, new ArrayItem(item.name)));
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
                UpArrayItem();

            if (GUILayout.Button('\u25BC'.ToString(), GUILayout.Width(buttonWidth)))
                DownArrayItem();

            if (GUILayout.Button("-", GUILayout.Width(buttonWidth)))
                RemoveArrayItem(openableEditor);

            EditorGUILayout.EndHorizontal();

            if (openableEditor.shown)
                openableEditor.editor.OnCustomInspectorGUI();

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
            AddArrayItem(selectedIndex, userItemName);

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
            throw new EditorException("User item name must be filled");

        Type selectedType = arrayItemTypes[selectedIndex];

        int newItemPosition = GetLastArrayItemPosition() + 1;
        ArrayItem newArrayItem = new ArrayItem(userItemName, new List<int> { newItemPosition }, selectedType);
        
        GameObject currParent = findOrCreateParentGameObject(target);

        GameObject addedGameObject = GameObjectManager.Add(currParent, newArrayItem.wholeName);
        addedGameObject.AddComponent(selectedType);

        RefreshEditors();
    }

    private void RemoveArrayItem(OpenableEditor<TEditor> openableEditor)
    {
        if (!GameObjectManager.TryFindChild(findOrCreateParentGameObject(target), openableEditor.arrayItem.wholeName, out GameObject gameObjectToRemove))
            throw new EditorException("Child you are trying to remove was not found");
        GameObjectManager.Remove(gameObjectToRemove);
        RefreshEditors();
    }

    private void UpArrayItem()
    {
        Debug.Log("Up array item");
    }

    private void DownArrayItem()
    {
        Debug.Log("Down array item");
    }

    public ArrayEditor(string parentGameObjectName)
    {
        this.parentGameObjectName = parentGameObjectName;
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

        return null;
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


