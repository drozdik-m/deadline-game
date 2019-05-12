using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConditionCollection))]
public class ConditionCollectionEditor : DefaultEditor<ConditionCollection>, IArrayItemEditor
{

    ArrayEditor<ConditionCollection, DesiredCondition, DesiredConditionEditor> condArrEditor;

    private void createReactionCollectionIfNotExists()
    {
        if (Target.reactionCollection != null) return;

        Component reactionCollectionComponent = Target.gameObject.GetComponent<ReactionCollection>();
        if (reactionCollectionComponent == null)
            Target.reactionCollection = Target.gameObject.AddComponent<ReactionCollection>();
        else
            Target.reactionCollection = (ReactionCollection)reactionCollectionComponent;
    }

    private void AddReactionCollectionOpt()
    {
        if (Target.reactionCollection == null)
        {
            EditorGUILayout.LabelField("Reaction Collection is null", WarningStyle);
            if (GUILayout.Button("Add Reaction Collection"))
                createReactionCollectionIfNotExists();
        }
    }

    public ConditionCollectionEditor()
    {
        condArrEditor = new ArrayEditor<ConditionCollection, DesiredCondition, DesiredConditionEditor>("DesiredConditions", MessageBox);
    }

    public string GetFoldoutLabel()
    {
        return "Condition Collection";
    }

    public override void OnCustomInspectorGUI()
    {
        AddReactionCollectionOpt();

        EditorGUILayout.Space();

        Target.reactionCollection = (ReactionCollection)EditorGUILayout.ObjectField("Reaction Collection",
                                                                                     Target.reactionCollection,
                                                                                     typeof(ReactionCollection),
                                                                                     true);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Required Conditions");
        Target.requiredConditions = condArrEditor.Use(Target);

        if (Target.requiredConditions == null || Target.requiredConditions.Length <= 0)
            MessageBox.AddMessage("There is no required condition", WarningStyle);
    }
}
