using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Interactable))]
public class InteractableEditor : DefaultEditor<Interactable>
{
    ArrayEditor<Interactable, ConditionCollection, ConditionCollectionEditor> condCollectionArrEditor;

    private void createReactionCollectionIfNotExists()
    {
        if (Target.defaultReactionCollection != null) return;

        Component reactionCollectionComponent = Target.gameObject.GetComponent<ReactionCollection>();
        if (reactionCollectionComponent == null)
            Target.defaultReactionCollection = Target.gameObject.AddComponent<ReactionCollection>();
    }

    private void AddDefReacColCompOpt()
    {
        if (Target.defaultReactionCollection == null)
        {
            EditorGUILayout.LabelField("Default Reaction Collection is null", WarningStyle);
            if (GUILayout.Button("Add Default Reaction Collection"))
                createReactionCollectionIfNotExists();
        }
    }

    private void ChangeTagOpt()
    {
        if (Target.gameObject.tag != "Interactable")
        {
            EditorGUILayout.LabelField("Game Object does not have 'Interactable' Tag.", WarningStyle);
            if (GUILayout.Button("Change Tag to 'Interactable'"))
                Target.gameObject.tag = "Interactable";
        }
    }

    private void AddBoxColliderOpt()
    {
        BoxCollider boxCollider = Target.gameObject.GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            EditorGUILayout.LabelField("Game Object does Box Collider", WarningStyle);
            if (GUILayout.Button("Add Box Collider"))
            {
                BoxCollider newBoxCollider = Target.gameObject.AddComponent<BoxCollider>();
                newBoxCollider.isTrigger = true;
            }
        }
        else if (!boxCollider.isTrigger)
        {
            EditorGUILayout.LabelField("Game Object's Box Collider is not trigger", WarningStyle);
            if (GUILayout.Button("Set Box Collider as Trigger"))
                Target.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    private void AddInteractionLocationOpt()
    {
        const string intLocGameObjectName = "InteractionLocation";
        if (!Target.useProximity && Target.interactionLocation == null)
        {
            EditorGUILayout.LabelField("Interaction location is null (add it, or use Proximity)", WarningStyle);
            if (GUILayout.Button("Add Interaction Location"))
            {
                if (!GameObjectManager.TryFindChild(Target.gameObject,
                                                   intLocGameObjectName,
                                                   out GameObject intLocGameObject))
                {
                    intLocGameObject = GameObjectManager.Add(Target.gameObject, intLocGameObjectName);
                }

                Target.interactionLocation = intLocGameObject.GetComponent<Transform>();
            }
        }
    }

    private void AddConnectedObjectOpt()
    {
        if (Target.useProximity && Target.connectedObject == null)
        {
            EditorGUILayout.LabelField("Connected Object (center for proximity) is null", WarningStyle);
            if (GUILayout.Button("Set parent as Connected Object"))
            {
                if (Target.gameObject.transform.parent != null)
                    Target.connectedObject = Target.gameObject.transform.parent.gameObject;
                else
                    Debug.LogError("Current GameObject does not have parent");
            }
        }
    }

    public InteractableEditor()
    {
        condCollectionArrEditor = 
            new ArrayEditor<Interactable, ConditionCollection, ConditionCollectionEditor>("ConditionCollections",
                                                                                          MessageBox);
    }

    public override void OnCustomInspectorGUI()
    {
        AddDefReacColCompOpt();
        ChangeTagOpt();
        AddBoxColliderOpt();
        AddInteractionLocationOpt();
        AddConnectedObjectOpt();

        // define header style
        GUIStyle headerStyle = new GUIStyle();
        headerStyle.fontSize = 13;
        headerStyle.normal.textColor = Color.white;

        EditorGUILayout.LabelField("Define location of interaction", headerStyle);
        EditorGUILayout.LabelField("Interaction Location or Proximity of Connected Object");
        EditorGUILayout.LabelField("-");

        Target.useProximity = EditorGUILayout.Toggle("Use Proximity", Target.useProximity);

        if (Target.useProximity)
        {
            Target.connectedObject = (GameObject)EditorGUILayout.ObjectField("Connected Object",
                                                                              Target.connectedObject,
                                                                              typeof(GameObject),
                                                                              true);
            Target.proximity = EditorGUILayout.Slider("Proximity", Target.proximity, 0, 10);
        }
        else
        {
            Target.interactionLocation = (Transform)EditorGUILayout.ObjectField("Interaction Location",
                                                                                Target.interactionLocation,
                                                                                typeof(Transform),
                                                                                true);
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Define Default Reaction Collection", headerStyle);
        EditorGUILayout.LabelField("Reactions that will be played as default");
        EditorGUILayout.LabelField("-");
        Target.defaultReactionCollection = (ReactionCollection)EditorGUILayout.ObjectField("Default Reaction Collection",
                                                                             Target.defaultReactionCollection,
                                                                             typeof(ReactionCollection),
                                                                             true);
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Define Condition Collections", headerStyle);
        EditorGUILayout.LabelField("Playing Reaction Collection if conditions are satisfied");
        EditorGUILayout.LabelField("-");
        Target.conditionCollections = condCollectionArrEditor.Use(Target);

        if (Target.proximity == 0 && Target.useProximity)
            MessageBox.AddMessage("Proximity is set to 0 (most likely is this wrong)", WarningStyle);
    }
}
