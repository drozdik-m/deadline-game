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
            ArrayItem currArrayItem = new ArrayItem(item.name);
            if (currArrayItem.type == typeof(EditorLink))
                subEditors.Add(new OpenableEditor<TEditor>(false, null, currArrayItem, true, item));
            else
                subEditors.Add(new OpenableEditor<TEditor>(false, Editor.CreateEditor(item.GetComponent<T>()) as TEditor, currArrayItem));
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

            if (openableEditor.isEditorLink)
                openableEditor.shown = EditorGUILayout.Foldout(openableEditor.shown, "[Editor Link] " + openableEditor.arrayItem.userName);
            else
                openableEditor.shown = EditorGUILayout.Foldout(openableEditor.shown, "[" + openableEditor.editor.GetFoldoutLabel() + "] " + openableEditor.arrayItem.userName);

            if (GUILayout.Button('\u25B2'.ToString(), GUILayout.Width(buttonWidth)))
                UpArrayItem(openableEditor);

            if (GUILayout.Button('\u25BC'.ToString(), GUILayout.Width(buttonWidth)))
                DownArrayItem(openableEditor);

            if (GUILayout.Button("-", GUILayout.Width(buttonWidth)))
                RemoveArrayItem(openableEditor);

            EditorGUILayout.EndHorizontal();

            if (openableEditor.shown)
            {
                if (openableEditor.isEditorLink)
                {
                    if (!(openableEditor.gameObjectItem == null))
                    {
                        EditorLinkEditor editorLinkEditor = Editor.CreateEditor(openableEditor.gameObjectItem.GetComponent<EditorLink>()) as EditorLinkEditor;
                        editorLinkEditor.OnInspectorGUI();
                    }
                }
                else
                    openableEditor.editor.OnInspectorGUI();
            }
                

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

        subTypeList.Insert(0, typeof(EditorLink));
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

        if (userItemName.Contains(' '))
        {
            Debug.LogWarning("User item name cannot contain spaces");
            return;
        }

        if (userItemName.Contains('[') || userItemName.Contains(']'))
        {
            Debug.LogWarning("User item name cannot contain '[' or ']'");
            return;
        }

        Type selectedType = arrayItemTypes[selectedIndex];

        int newItemPosition = GetLastArrayItemPosition() + 1;
        ArrayItem newArrayItem = new ArrayItem(userItemName, new List<int> { newItemPosition }, selectedType);

        GameObject currParent = findOrCreateParentGameObject(target);
        currParent.AddComponent(newArrayItem.type);

        var testArrayItemTarget = currParent.GetComponent(newArrayItem.type);
        Editor testArrayItemInterfaceEditor = Editor.CreateEditor(testArrayItemTarget);
        
        if (!(testArrayItemInterfaceEditor is IArrayItemEditor))
        {
            changeLastMessage("Item type that you are trying to create does not have editor", DefaultEditor<MonoBehaviour>.ErrorStyle);
            UnityEngine.Object.DestroyImmediate(currParent.GetComponent(newArrayItem.type));
            return;
        }
        UnityEngine.Object.DestroyImmediate(currParent.GetComponent(newArrayItem.type));

        GameObject addedGameObject = GameObjectManager.Add(currParent, newArrayItem.wholeName);
        addedGameObject.AddComponent(selectedType);

        changeLastMessage("Item (" + newArrayItem.userName + ") added", DefaultEditor<MonoBehaviour>.SuccessStyle);
        GameObjectManager.SortChildrenAlphabetically(currParent);
        RefreshEditors();
    }

    private void RemoveArrayItem(OpenableEditor<TEditor> openableEditor)
    {
        GameObject currParent = findOrCreateParentGameObject(target);
        if (!GameObjectManager.TryFindChild(currParent, openableEditor.arrayItem.wholeName, out GameObject gameObjectToRemove))
            throw new EditorException("Parent of child you are trying to remove was not found");
        GameObjectManager.Remove(gameObjectToRemove);

        changeLastMessage("Item (" + openableEditor.arrayItem.userName + ") removed", DefaultEditor<MonoBehaviour>.SuccessStyle);
        GameObjectManager.SortChildrenAlphabetically(currParent);
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
        GameObject currParent = findOrCreateParentGameObject(target);
        if (!GameObjectManager.TryFindChild(currParent, openableEditor.arrayItem.wholeName, out GameObject foundUp))
            throw new EditorException("Specified game object to move was not found");
        foundUp.name = newCurrentToUp.wholeName;

        // change down
        if (!GameObjectManager.TryFindChild(findOrCreateParentGameObject(target), firstBefore.arrayItem.wholeName, out GameObject foundDown))
            throw new EditorException("Specified game object to move was not found");
        foundDown.name = newToDown.wholeName;

        GameObjectManager.SortChildrenAlphabetically(currParent);

        changeLastMessage("Item (" + openableEditor.arrayItem.userName + ") moved up", DefaultEditor<MonoBehaviour>.SuccessStyle);
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
        GameObject currParent = findOrCreateParentGameObject(target);
        if (!GameObjectManager.TryFindChild(currParent, openableEditor.arrayItem.wholeName, out GameObject foundDown))
            throw new EditorException("Specified game object to move was not found");
        foundDown.name = newCurrentToDown.wholeName;

        // change down
        if (!GameObjectManager.TryFindChild(findOrCreateParentGameObject(target), firstAfter.arrayItem.wholeName, out GameObject foundUp))
            throw new EditorException("Specified game object to move was not found");
        foundUp.name = newToUp.wholeName;

        GameObjectManager.SortChildrenAlphabetically(currParent);

        changeLastMessage("Item (" + openableEditor.arrayItem.userName + ") moved down", DefaultEditor<MonoBehaviour>.SuccessStyle);
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

            if (currType == typeof(EditorLink))
            {
                EditorLink currEditorLink = foundGameObject.GetComponent<EditorLink>();

                if (currEditorLink.linkedGameObject == null)
                {
                    editorMessageBox.AddMessage("Editor Link (" + subEditors[i].arrayItem.userName + ") does not have linked GameObject", DefaultEditor<MonoBehaviour>.WarningStyle);
                    newArr[i] = null;
                }
                else
                {
                    GameObject linkedGameObject = currEditorLink.linkedGameObject;
                    ArrayItem linkedGameObjectArrayItem = new ArrayItem(linkedGameObject.name);
                    newArr[i] = linkedGameObject.GetComponent(linkedGameObjectArrayItem.type) as T;
                }
            }
            else
            {
                Component currComponent = foundGameObject.GetComponent(currType);
                if (currComponent == null)
                    newArr[i] = null;
                else
                    newArr[i] = foundGameObject.GetComponent(currType) as T;
            }
        }

        // remove null links
        newArr = newArr.Where(i => i != null).ToArray();
        return newArr;
    }
}

public class OpenableEditor<TEditor>
{
    public bool shown;
    public TEditor editor;
    public ArrayItem arrayItem;
    public bool isEditorLink;
    public GameObject gameObjectItem;

    public OpenableEditor(bool shown, TEditor editor, ArrayItem arrayItem, bool isEditorLink = false, GameObject gameObjectItem = null)
    {
        this.shown = shown;
        this.editor = editor;
        this.arrayItem = arrayItem;
        this.isEditorLink = isEditorLink;
        this.gameObjectItem = gameObjectItem;
    }
}