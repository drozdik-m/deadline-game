using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageConditionalSubManager))]
public class StageConditionalSubManagerEditor : StageEditor
{
    readonly OverrideMonoscriptField<StageManager> stageManagerField = new OverrideMonoscriptField<StageManager>("Override stage manager");

    public override string GetFoldoutLabel()
    {
        return "StageConditionalSubManager";
    }

    public override void OnStageInspectorGUI()
    {
        var Target = base.Target as StageConditionalSubManager;

        //Stage manager
        Target.StageManager = stageManagerField.Render(Target.StageManager);
        stageManagerField.CheckForNullOverride(Target.StageManager, MessageBox, "Override stage manager not set");
        if (!stageManagerField.OverrideChecked && Target.GetComponent<StageManager>() == null)
        {
            MessageBox.AddMessage("This GameObject does not have StageManager component", ErrorStyle);
            if (GUILayout.Button("Add StageManager component"))
                Target.gameObject.AddComponent<StageManager>();
        }

        //Condition
        Target.Condition = EditorGUILayout.ObjectField("Game condition", Target.Condition, typeof(Condition), true) as Condition;
        if (Target.Condition == null)
            MessageBox.AddMessage("Condition is null. The subStageManager will never be launched", ErrorStyle);
        else
            EditorGUILayout.LabelField("Condition is " + Target.Condition.satisfied);
        
    }
}
