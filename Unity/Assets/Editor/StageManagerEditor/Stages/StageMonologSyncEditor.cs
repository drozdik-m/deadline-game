using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageMonologSync))]
public class StageMonologSyncEditor : StageEditor
{
    OverrideMonoscriptField<DialogManager> dialog = new OverrideMonoscriptField<DialogManager>("Use specific dialog", "Specific dialog");

    public override string GetFoldoutLabel()
    {
        return "StageMonologSync";
    }

    public override void OnStageInspectorGUI()
    {
        //DrawDefaultInspector();

        StageMonologSync Target = base.Target as StageMonologSync;

        //Dialog
        Target.DialogManager = dialog.Render(Target.DialogManager);
        dialog.CheckForNullOverride(Target.DialogManager, MessageBox, "Override dialog is null");
        if (!dialog.OverrideChecked)
        {
            if (GameObject.FindGameObjectWithTag("DialogManager") == null)
                MessageBox.AddMessage("DialogManager not found", ErrorStyle);
        }

        //What to say
        Target.WhatToSay = EditorGUILayout.ObjectField("What to say", Target.WhatToSay, typeof(SelfTalkDialog), true) as SelfTalkDialog;
        if (Target.WhatToSay == null)
            MessageBox.AddMessage("WhatToSay object is null", ErrorStyle);
    }

}
