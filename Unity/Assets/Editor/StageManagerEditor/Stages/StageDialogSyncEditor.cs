using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageDialogSync))]
public class StageDialogSyncEditor : StageEditor
{
    OverrideMonoscriptField<DialogManager> dialog = new OverrideMonoscriptField<DialogManager>("Use specific dialog manager", "Specific dialog");

    public override string GetFoldoutLabel()
    {
        return "StageDialogSync";
    }

    public override void OnStageInspectorGUI()
    {
        //DrawDefaultInspector();

        StageDialogSync Target = base.Target as StageDialogSync;

        //Dialog
        Target.DialogManager = dialog.Render(Target.DialogManager);
        dialog.CheckForNullOverride(Target.DialogManager, MessageBox, "Override dialog is null");
        if (!dialog.OverrideChecked)
        {
            if (GameObject.FindGameObjectWithTag("DialogManager") == null)
                MessageBox.AddMessage("DialogManager not found", ErrorStyle);
        }

        //What to say
        Target.WhatToSay = EditorGUILayout.ObjectField("What to say", Target.WhatToSay, typeof(TwinTalkDialog), true) as TwinTalkDialog;
        if (Target.WhatToSay == null)
            MessageBox.AddMessage("WhatToSay object is null", ErrorStyle);

        //Who to talk to
        Target.DialogTarget = EditorGUILayout.ObjectField("To whom to talk", Target.DialogTarget, typeof(GameObject), true) as GameObject;
        if (Target.DialogTarget == null)
            MessageBox.AddMessage("Dialog target is null", ErrorStyle);
    }

}
