using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom editor for AllConditions
/// </summary>
[CustomEditor(typeof(AllConditions))]
public class AllConditionsEditor : Editor
{
    private static string[] allConditionDescriptions;
    private ConditionEditor[] conditionEditors;
    private AllConditions allConditions;
    private string newConditionDescription = "New Condition";
    private const string creationPath = "Assets/Resources/AllConditions.asset";
    private const float buttonWidth = 30f;

    public static string[] AllConditionDescriptions
    {
        get
        {
            if (allConditionDescriptions == null)
            {
                SetAllConditionDescriptions();
            }
            return allConditionDescriptions;
        }
        private set { allConditionDescriptions = value; }
    }

    private void OnEnable()
    {
        allConditions = (AllConditions)target;

        if (allConditions.conditions == null)
            allConditions.conditions = new Condition[0];

        if (conditionEditors == null)
            CreateEditors();
    }

    private void OnDisable()
    {
        for (int i = 0; i < conditionEditors.Length; i++)
            DestroyImmediate(conditionEditors[i]);

        conditionEditors = null;
    }

    private static void SetAllConditionDescriptions()
    {
        AllConditionDescriptions = new string[TryGetConditionsLength()];

        for (int i = 0; i < AllConditionDescriptions.Length; i++)
            AllConditionDescriptions[i] = TryGetConditionAt(i).description;
    }

    public override void OnInspectorGUI()
    {
        if (conditionEditors.Length != TryGetConditionsLength())
        {
            for (int i = 0; i < conditionEditors.Length; i++)
                DestroyImmediate(conditionEditors[i]);
            CreateEditors();
        }

        for (int i = 0; i < conditionEditors.Length; i++)
        {
            conditionEditors[i].OnInspectorGUI();
            //Debug.Log(i);
        }

        if (TryGetConditionsLength() > 0)
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        EditorGUILayout.BeginHorizontal();
        newConditionDescription = EditorGUILayout.TextField(GUIContent.none, newConditionDescription);
        if (GUILayout.Button("+", GUILayout.Width(buttonWidth)))
        {
            AddCondition(newConditionDescription);
            newConditionDescription = "New Condition";
        }
        EditorGUILayout.EndHorizontal();
    }

    private void CreateEditors()
    {
        conditionEditors = new ConditionEditor[allConditions.conditions.Length];

        for (int i = 0; i < conditionEditors.Length; i++)
        {
            conditionEditors[i] = CreateEditor(TryGetConditionAt(i)) as ConditionEditor;
            conditionEditors[i].editorType = ConditionEditor.EditorType.AllConditionAsset;
        }
    }

    [MenuItem("Assets/Create/AllConditions")]
    private static void CreateAllConditionsAsset()
    {
        if (AllConditions.Instance)
            return;

        AllConditions instance = CreateInstance<AllConditions>();
        AssetDatabase.CreateAsset(instance, creationPath);
        AllConditions.Instance = instance;
        instance.conditions = new Condition[0];
    }

    private void AddCondition(string description)
    {
        if (!AllConditions.Instance)
        {
            Debug.LogError("AllConditions has not been created yet.");
            return;
        }

        Condition newCondition = ConditionEditor.CreateCondition(description);
        newCondition.name = description;

        Undo.RecordObject(newCondition, "Created new Condition");
   
        AssetDatabase.AddObjectToAsset(newCondition, AllConditions.Instance);
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newCondition));

        ArrayUtility.Add(ref AllConditions.Instance.conditions, newCondition);

        EditorUtility.SetDirty(AllConditions.Instance);

        SetAllConditionDescriptions();
    }

    public static void RemoveCondition(Condition condition)
    {
        if (!AllConditions.Instance)
        {
            Debug.LogError("AllConditions has not been created yet.");
            return;
        }

        Undo.RecordObject(AllConditions.Instance, "Removing condition");

        ArrayUtility.Remove(ref AllConditions.Instance.conditions, condition);

        DestroyImmediate(condition, true);
        AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(AllConditions.Instance);

        SetAllConditionDescriptions();
    }

    public static int TryGetConditionIndex(Condition condition)
    {
        for (int i = 0; i < TryGetConditionsLength(); i++)
            if (CompareConditions(TryGetConditionAt(i), condition))
                return i;
    
        return -1;
    }

    public static Condition TryGetConditionAt(int index)
    {
        Condition[] allConditions = AllConditions.Instance.conditions;

        if (allConditions == null)
            return null;
        if (allConditions[0] == null)
            return null;

        if (index >= allConditions.Length)
            return allConditions[0];

        return allConditions[index];
    }

    public static int TryGetConditionsLength()
    {
        if (AllConditions.Instance.conditions == null)
            return 0;

        return AllConditions.Instance.conditions.Length;
    }

    public static bool CompareConditions(Condition c1, Condition c2)
    {
        Debug.Log("Cond1: " + c1.description + ", val: " + c1.satisfied);
        Debug.Log("Cond2: " + c2.description + ", val: " + c2.satisfied);
        if (c1.satisfied != c2.satisfied) return false;
        return c1.description == c2.description;
    }
}
