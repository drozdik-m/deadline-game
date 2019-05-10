using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestMoveThereCondition))]
public class QuestMoveThereConditionEditor : QuestConditionEditor
{
    OverrideMonoscriptField<CollisionEvent> triggerField = new OverrideMonoscriptField<CollisionEvent>
        ("Use specific condition editor", "Condition Editor");

    public override string GetFoldoutLabel()
    {
        return "QuestMoveThereCondition";
    }

    public override void OnConditionInspectorGUI()
    {
        //DrawDefaultInspector();

        QuestMoveThereCondition Target = base.Target as QuestMoveThereCondition;

        Target.CollisionTrigger = triggerField.Render(Target.CollisionTrigger);
        triggerField.CheckForNullOverride(Target.CollisionTrigger, MessageBox, "Collision event not set");
        if (!triggerField.OverrideChecked && Target.gameObject.GetComponent<CollisionEvent>() == null)
        {
            MessageBox.AddMessage("CollisionEvent not found on this component", ErrorStyle);
            if (GUILayout.Button("Add CollisionEvent"))
                Target.gameObject.AddComponent<CollisionEvent>();
            if (GUILayout.Button("Create CollisionEvent child component"))
            {
                GameObject createdGO = GameObjectManager.Add(Target.gameObject, "[CollisionEvent]");
                createdGO.AddComponent<CollisionEvent>();
                createdGO.AddComponent<BoxCollider>();
                Target.CollisionTrigger = createdGO.GetComponent<CollisionEvent>();
            }
        }

    }
}
