using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageSubManager))]
public class StageSubManagerEditor : StageEditor
{
    OverrideMonoscriptField<StageManager> stageManagerField = new OverrideMonoscriptField<StageManager>("Override stage manager");

    public override string GetFoldoutLabel()
    {
        return "StageSubManager";
    }

    public override void OnStageInspectorGUI()
    {
        StageSubManager Target = base.Target as StageSubManager;

        //Stage manager
        Target.StageManager = stageManagerField.Render(Target.StageManager);
        stageManagerField.CheckForNullOverride(Target.StageManager, MessageBox, "Override stage manager not set");
        if (!stageManagerField.OverrideChecked && Target.GetComponent<StageManager>() == null)
        {
            MessageBox.AddMessage("This GameObject does not have StageManager component", ErrorStyle);
            if (GUILayout.Button("Add StageManager component"))
                Target.gameObject.AddComponent<StageManager>();
        }

    }
}
