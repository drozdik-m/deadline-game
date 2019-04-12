using UnityEditor;
using UnityEngine;

/// <summary>
/// custom editor for interactables
/// </summary>
[CustomEditor(typeof(Interactable))]
public class InteractableEditor : EditorWithSubEditors<ConditionCollectionEditor, ConditionCollection>
{
    private const float PROXIMITY_FROM = 0;
    private const float PROXIMITY_TO = 20;

    private Interactable interactable;

    private SerializedProperty interactionLocationProperty;
    private SerializedProperty collectionsProperty;
    private SerializedProperty defaultReactionCollectionProperty;
    private SerializedProperty connectedObjectProperty;
    private SerializedProperty hasLocationOnProximityProperty;
    private SerializedProperty proximityProperty;

    private const float collectionButtonWidth = 125f;
    private const string interactablePropInteractionLocationName = "interactionLocation";
    private const string interactablePropConditionCollectionsName = "conditionCollections";
    private const string interactablePropDefaultReactionCollectionName = "defaultReactionCollection";
    private const string interactablePropConnectedObjectPropertyName = "connectedObject";
    private const string interactablePropHasLocationOnProximityName = "useProximity";
    private const string interactableProximityName = "proximity";

    private void OnEnable()
    {
        collectionsProperty = serializedObject.FindProperty(interactablePropConditionCollectionsName);
        interactionLocationProperty = serializedObject.FindProperty(interactablePropInteractionLocationName);
        defaultReactionCollectionProperty = serializedObject.FindProperty(interactablePropDefaultReactionCollectionName);
        connectedObjectProperty = serializedObject.FindProperty(interactablePropConnectedObjectPropertyName);
        hasLocationOnProximityProperty = serializedObject.FindProperty(interactablePropHasLocationOnProximityName);
        proximityProperty = serializedObject.FindProperty(interactableProximityName);

        interactable = (Interactable)target;

        CheckAndCreateSubEditors(interactable.conditionCollections);
    }

    private void OnDisable()
    {
        CleanupEditors();
    }

    protected override void SubEditorSetup(ConditionCollectionEditor editor)
    {
        editor.collectionsProperty = collectionsProperty;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        CheckAndCreateSubEditors(interactable.conditionCollections);

        EditorGUILayout.PropertyField(interactionLocationProperty);

        // condition collections
        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i].OnInspectorGUI();
            EditorGUILayout.Space();
        }

        EditorGUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add Collection",
                GUILayout.Width(collectionButtonWidth)))
        {
            ConditionCollection newCollection =
                ConditionCollectionEditor.CreateConditionCollection();
            collectionsProperty.AddToObjectArray(newCollection);
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(defaultReactionCollectionProperty);

        EditorGUILayout.LabelField("---");
        EditorGUILayout.LabelField("Use Proximity (optional)");
        EditorGUILayout.LabelField("Set 'Use Proximity' to true");
        EditorGUILayout.LabelField("Interaction Location will be ignored");
        EditorGUILayout.LabelField("---");

        EditorGUILayout.PropertyField(hasLocationOnProximityProperty);
        EditorGUILayout.PropertyField(connectedObjectProperty);
        EditorGUILayout.Slider(proximityProperty, PROXIMITY_FROM, PROXIMITY_TO);

        serializedObject.ApplyModifiedProperties();
    }
}