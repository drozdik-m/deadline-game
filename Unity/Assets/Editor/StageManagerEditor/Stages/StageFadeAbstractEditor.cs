using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public abstract class StageFadeAbstractEditor : StageEditor
{
    OverrideMonoscriptField<FaderController> fader = new OverrideMonoscriptField<FaderController>("Use specific Fader", "Override fader");


    public override void OnStageInspectorGUI()
    {
        //DrawDefaultInspector();

        StageFadeAbstract Target = base.Target as StageFadeAbstract;

        Target.Fader = fader.Render(Target.Fader);
        fader.CheckForNullOverride(Target.Fader, MessageBox, "Override fader is not set");

        if (!fader.OverrideChecked && GameObject.FindGameObjectWithTag("Fader") == null)
            MessageBox.AddMessage("Fader not found", ErrorStyle);

        OnFadeInspectorGUI();
    }

    public abstract void OnFadeInspectorGUI();
}
