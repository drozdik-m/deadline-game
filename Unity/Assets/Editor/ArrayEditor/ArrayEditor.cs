using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Array editor is class that creates subeditors for items in array
/// </summary>
/// <typeparam name="P">Wrapper of items type</typeparam>
/// <typeparam name="T">Item type</typeparam>
/// <typeparam name="TEditor">Item editor type</typeparam>
public class ArrayEditor<P, T, TEditor>
    where P : MonoBehaviour
    where T : MonoBehaviour
    where TEditor : Editor, IArrayItemEditor
{
    #region fields
    
    // fields for game object object orientation
    private P target;
    private string parentGameObjectUserName;
    private List<OpenableEditor<TEditor>> subEditors;
    private string userItemName = "";

    // fields for showing help messages
    private string lastMessage = "";
    private GUIStyle lastMessageStyle = GUIStyle.none;
    private EditorMessageBox editorMessageBox;

    // fields for creating type list
    private Type[] arrayItemTypes;
    private string[] arrayItemTypeNames;
    private int selectedIndex;

    // fields for styling
    private float buttonWidth = 30f;

    #endregion

    #region private methods
    private string createFoldoutName(string typeStr, string username)
    {
        return $"[{typeStr}] {username}";
    }

    private void changeLastMessage(string msg, GUIStyle style)
    {
        lastMessage = msg;
        lastMessageStyle = style;
    }

    private void DestroySubEditors()
    {
        if (subEditors != null)
            foreach (var editor in subEditors)
                UnityEngine.Object.DestroyImmediate(editor.editor);

        subEditors = new List<OpenableEditor<TEditor>>();
    }

    private void LoadEditorsFromHierarchy()
    {
        GameObject parentGameObject = findOrCreateParentGameObject(target);
        GameObject[] existingItems = GameObjectManager.GetChildren(parentGameObject);

        foreach (GameObject item in existingItems)
        {
            ArrayItem currArrayItem = new ArrayItem(item.name);

            // if we have editor link, we have to create special openable editor
            if (currArrayItem.type == typeof(EditorLink))
            {
                subEditors.Add(new OpenableEditor<TEditor>(false,
                                                           null,
                                                           currArrayItem,
                                                           true,
                                                           item));
            }
            // otherwise create openable editor based on type
            else
            {
                subEditors.Add(new OpenableEditor<TEditor>(false,
                                                           Editor.CreateEditor(item.GetComponent<T>()) as TEditor,
                                                           currArrayItem));
            }
        }

        // sort sub editors by position
        subEditors = subEditors.OrderBy(s => s.arrayItem.positions.First()).ToList();
    }

    private void InitLinkEditor(OpenableEditor<TEditor> openableEditor)
    {
        if (!(openableEditor.gameObjectItem == null))
        {
            EditorLinkEditor editorLinkEditor = Editor.CreateEditor(openableEditor.gameObjectItem.GetComponent<EditorLink>()) as EditorLinkEditor;
            editorLinkEditor.OnInspectorGUI();
        }
    }

    private void InitSubEditors()
    {
        // show all editors
        foreach (var openableEditor in subEditors)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;

            EditorGUILayout.BeginHorizontal();

            if (openableEditor.isEditorLink)
            {
                openableEditor.shown = EditorGUILayout.Foldout(
                    openableEditor.shown,
                    createFoldoutName(EditorLink.FoldoutName,
                                      openableEditor.arrayItem.userName)
                );
            }
            else
            {
                openableEditor.shown = EditorGUILayout.Foldout(
                    openableEditor.shown,
                    createFoldoutName(openableEditor.editor.GetFoldoutLabel(),
                                      openableEditor.arrayItem.userName)
                );
            }

            if (GUILayout.Button('\u25B2'.ToString(), GUILayout.Width(buttonWidth)))
                UpArrayItem(openableEditor);

            if (GUILayout.Button('\u25BC'.ToString(), GUILayout.Width(buttonWidth)))
                DownArrayItem(openableEditor);

            if (GUILayout.Button("-", GUILayout.Width(buttonWidth)))
                RemoveArrayItem(openableEditor);

            EditorGUILayout.EndHorizontal();

            // if editor is tagged as opened, show it
            if (openableEditor.shown)
            {
                if (openableEditor.isEditorLink)
                    InitLinkEditor(openableEditor);
                else
                    openableEditor.editor.OnInspectorGUI();
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }

        // create add item gui
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
        string parentGameObjectFullName = "_" + typeOfTarget.ToString() + "_" + parentGameObjectUserName;

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
        // get all sub classes of T
        Type type = typeof(T);
        Type[] allTypes = type.Assembly.GetTypes();
        List<Type> subTypeList = new List<Type>();

        for (int i = 0; i < allTypes.Length; i++)
            if (allTypes[i].IsSubclassOf(type) && !allTypes[i].IsAbstract)
                subTypeList.Add(allTypes[i]);

        subTypeList.Insert(0, typeof(EditorLink)); // add editor link to the start
        arrayItemTypes = subTypeList.ToArray();

        // create string array for dropdown menu
        List<string> typeNameList = new List<string>();
        for (int i = 0; i < arrayItemTypes.Length; i++)
            typeNameList.Add(arrayItemTypes[i].Name);
        arrayItemTypeNames = typeNameList.ToArray();
    }

    private void AddItemGUI()
    {
        EditorGUILayout.Space();

        GUILayout.BeginVertical(EditorStyles.helpBox);

        // define header style
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
        // iterate through all openable editors
        // and get the last position
        // if there is no item, return 1

        int lastPosition = 1;

        foreach (var openableEditor in subEditors)
        {
            int maxPos = openableEditor.arrayItem.positions.Max();
            if (maxPos > lastPosition)
                lastPosition = maxPos;
        }

        return lastPosition;
    }

    private bool ValidateAddingItem(int chosenTypeIndex, string itemUsername)
    {

        if (string.IsNullOrWhiteSpace(itemUsername))
        {
            Debug.LogWarning("User item name must be filled");
            return false;
        }

        if (itemUsername.Contains(' '))
        {
            Debug.LogWarning("User item name cannot contain spaces");
            return false;
        }

        if (itemUsername.Contains('[') || itemUsername.Contains(']'))
        {
            Debug.LogWarning("User item name cannot contain '[' or ']'");
            return false;
        }

        if (chosenTypeIndex < 0 || chosenTypeIndex >= arrayItemTypes.Length)
            throw new EditorException("Selected type is not valid");

        return true;
    }

    private bool TestEditorTypeExistence(GameObject currParent, ArrayItem newArrayItem,
                                         string itemUsername, int newItemPos, Type chosenType)
    {
        currParent.AddComponent(newArrayItem.type);

        var testArrayItemTarget = currParent.GetComponent(newArrayItem.type);
        Editor testArrayItemInterfaceEditor = Editor.CreateEditor(testArrayItemTarget);

        if (!(testArrayItemInterfaceEditor is IArrayItemEditor))
        {
            changeLastMessage("Item type that you are trying to create does not have editor",
                               DefaultEditor<MonoBehaviour>.ErrorStyle);

            UnityEngine.Object.DestroyImmediate(currParent.GetComponent(newArrayItem.type));
            return false;
        }
        UnityEngine.Object.DestroyImmediate(currParent.GetComponent(newArrayItem.type));

        return true;
    }

    // prints message, sorts children and refreshes editor
    private void FinishOperation(GameObject currParent, string msg)
    {
        changeLastMessage(msg, DefaultEditor<MonoBehaviour>.SuccessStyle);
        GameObjectManager.SortChildrenAlphabetically(currParent);
        RefreshEditors();
    }

    private void AddArrayItem(int chosenTypeIndex, string itemUsername)
    {
        if (!ValidateAddingItem(chosenTypeIndex, itemUsername)) return;

        GameObject currParent = findOrCreateParentGameObject(target);
        Type chosenType = arrayItemTypes[chosenTypeIndex];
        int newItemPos = GetLastArrayItemPosition() + 1;
        ArrayItem newArrayItem = new ArrayItem(itemUsername,
                                               new List<int> { newItemPos },
                                               chosenType);

        // check if the editor for this type of object exists
        if (!TestEditorTypeExistence(currParent,
                                     newArrayItem,
                                     itemUsername,
                                     newItemPos,
                                     chosenType))
        {
            return;
        }

        GameObject addedGameObject = GameObjectManager.Add(currParent,
                                                           newArrayItem.wholeName);
        addedGameObject.AddComponent(chosenType);

        FinishOperation(currParent, "Item (" + newArrayItem.userName + ") added");
    }

    private void RemoveArrayItem(OpenableEditor<TEditor> openableEditor)
    {
        GameObject currParent = findOrCreateParentGameObject(target);
        if (!GameObjectManager.TryFindChild(currParent, openableEditor.arrayItem.wholeName, out GameObject gameObjectToRemove))
            throw new EditorException("Parent of child you are trying to remove was not found");
        GameObjectManager.Remove(gameObjectToRemove);

        FinishOperation(currParent, "Item (" + openableEditor.arrayItem.userName + ") removed");
    }

    private void UpArrayItem(OpenableEditor<TEditor> openableEditor)
    {
        // check if the item is not already at the mocs top position
        int minPos = subEditors.Min(s => s.arrayItem.positions.Min());
        if (openableEditor.arrayItem.positions.Min() == minPos)
            return;

        // get the previous item
        var allOtherSubEditors = subEditors.Where(s => s != openableEditor).ToList();
        OpenableEditor<TEditor> firstBefore = subEditors
            .Where(
                s => s != openableEditor &&
                s.arrayItem.positions.First() == subEditors.Min(se => se.arrayItem.positions.First()))
            .First();

        foreach (var otherEditor in allOtherSubEditors)
        {
            int currPos = otherEditor.arrayItem.positions.First();
            if (currPos > firstBefore.arrayItem.positions.First() &&
                currPos < openableEditor.arrayItem.positions.First())
            {
                firstBefore = otherEditor;
            }
        }

        ArrayItem newCurrentToUp = new ArrayItem(openableEditor.arrayItem.userName,
                                                 new List<int> { firstBefore.arrayItem.positions.First() },
                                                 openableEditor.arrayItem.type);

        ArrayItem newToDown = new ArrayItem(firstBefore.arrayItem.userName,
                                            new List<int> { openableEditor.arrayItem.positions.First() },
                                            firstBefore.arrayItem.type);

        // change up
        GameObject currParent = findOrCreateParentGameObject(target);
        if (!GameObjectManager.TryFindChild(currParent,
                                            openableEditor.arrayItem.wholeName,
                                            out GameObject foundUp))
        {
            throw new EditorException("Specified game object to move was not found");
        }

        foundUp.name = newCurrentToUp.wholeName;

        // change down
        if (!GameObjectManager.TryFindChild(findOrCreateParentGameObject(target),
                                            firstBefore.arrayItem.wholeName,
                                            out GameObject foundDown))
        {
            throw new EditorException("Specified game object to move was not found");
        }

        foundDown.name = newToDown.wholeName;

        FinishOperation(currParent, "Item (" + openableEditor.arrayItem.userName + ") moved up");
    }

    private void DownArrayItem(OpenableEditor<TEditor> openableEditor)
    {
        // check if the item is not already at the most bottom position
        int maxPos = subEditors.Max(s => s.arrayItem.positions.Max());
        if (openableEditor.arrayItem.positions.Max() == maxPos)
        {
            return;
        }

        // get the next item
        var allOtherSubEditors = subEditors.Where(s => s != openableEditor).ToList();
        OpenableEditor<TEditor> firstAfter = subEditors
            .Where(
              s => s != openableEditor &&
              s.arrayItem.positions.First() == subEditors.Max(a => a.arrayItem.positions.First()))
            .First();

        foreach (var otherEditor in allOtherSubEditors)
        {
            int currPos = otherEditor.arrayItem.positions.First();
            if (currPos < firstAfter.arrayItem.positions.First() &&
                currPos > openableEditor.arrayItem.positions.First())
            {
                firstAfter = otherEditor;
            }
        }

        ArrayItem newCurrentToDown = new ArrayItem(openableEditor.arrayItem.userName,
                                                   new List<int> { firstAfter.arrayItem.positions.First() },
                                                   openableEditor.arrayItem.type);

        ArrayItem newToUp = new ArrayItem(firstAfter.arrayItem.userName,
                                          new List<int> { openableEditor.arrayItem.positions.First() },
                                          firstAfter.arrayItem.type);

        // change up
        GameObject currParent = findOrCreateParentGameObject(target);
        if (!GameObjectManager.TryFindChild(currParent,
                                            openableEditor.arrayItem.wholeName,
                                            out GameObject foundDown))
        {
            throw new EditorException("Specified game object to move was not found");
        }

        foundDown.name = newCurrentToDown.wholeName;

        // change down
        if (!GameObjectManager.TryFindChild(findOrCreateParentGameObject(target),
                                            firstAfter.arrayItem.wholeName,
                                            out GameObject foundUp))
        {
            throw new EditorException("Specified game object to move was not found");
        }

        foundUp.name = newToUp.wholeName;

        FinishOperation(currParent, "Item (" + openableEditor.arrayItem.userName + ") moved down");
    }
    #endregion

    /// <summary>
    /// Constructs ArrayEditor
    /// </summary>
    /// <param name="parentGameObjectName">Parent object name that user choose</param>
    /// <param name="editorMessageBox">Editor message box for showing messages</param>
    public ArrayEditor(string parentGameObjectName, EditorMessageBox editorMessageBox = null)
    {
        this.parentGameObjectUserName = parentGameObjectName;
        this.editorMessageBox = editorMessageBox;
        subEditors = new List<OpenableEditor<TEditor>>();
        SetItemNamesArray();
    }

    /// <summary>
    /// Creates sub editors for given objects (special editors implementing IArrayItem required)
    /// </summary>
    /// <param name="sendTarget">Target object (collection wrapper)</param>
    /// <returns>Array of the created objects</returns>
    public T[] Use(P sendTarget)
    {
        target = sendTarget; 

        // if we have sub editors loaded, just initialize them, if not, load them and initialize them
        if (subEditors != null && subEditors.Count > 0)
            InitSubEditors();
        else
            RefreshEditors();

        // display message
        if (!string.IsNullOrWhiteSpace(lastMessage))
            editorMessageBox.AddMessage(lastMessage, lastMessageStyle);

        T[] newArr = new T[subEditors.Count];

        // create object array from game objects
        for (int i = 0; i < subEditors.Count; i++)
        {
            if (!GameObjectManager.TryFindChild(findOrCreateParentGameObject(target),
                                                subEditors[i].arrayItem.wholeName,
                                                out GameObject foundGameObject))
            {
                throw new EditorException("Child was not found");
            }
                
            Type currType = subEditors[i].arrayItem.type;

            // convert editor link to linked object (if linked object is null -> ignore it)
            if (currType == typeof(EditorLink))
            {
                EditorLink currEditorLink = foundGameObject.GetComponent<EditorLink>();

                if (currEditorLink.linkedGameObject == null)
                {
                    editorMessageBox.AddMessage("Editor Link (" + subEditors[i].arrayItem.userName +
                                                    ") does not have linked GameObject",
                                                DefaultEditor<MonoBehaviour>.WarningStyle);
                    newArr[i] = null;
                }
                else
                {
                    GameObject linkedGameObject = currEditorLink.linkedGameObject;
                    ArrayItem linkedGameObjectArrayItem = new ArrayItem(linkedGameObject.name);
                    newArr[i] = linkedGameObject.GetComponent(linkedGameObjectArrayItem.type) as T;
                }
            }
            // get component of created game object and save it
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

/// <summary>
/// Class used to work with openable editors
/// </summary>
/// <typeparam name="TEditor">Abstract editor type</typeparam>
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