using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageInteracted))]
public class StageInteractedEditor : StageEditor
{
    OverrideMonoscriptField<InteractionEventMiddleman> middlemanField = new OverrideMonoscriptField<InteractionEventMiddleman>("Interaction middleman is on different GameObject", "New middleman");

    public override string GetFoldoutLabel()
    {
        return "StageInteracted";
    }

    public override void OnStageInspectorGUI()
    {
        //DrawDefaultInspector();

        StageInteracted Target = base.Target as StageInteracted;

        Target.InteractionEventMiddleman = EditorGUILayout.ObjectField("Inveration Event Middleman", Target.InteractionEventMiddleman, typeof(InteractionEventMiddleman), true) as InteractionEventMiddleman;
        if (Target.InteractionEventMiddleman == null)
            MessageBox.AddMessage("Middleman is null", ErrorStyle);

        /*Target.InteractionEventMiddleman = middlemanField.Render(Target.InteractionEventMiddleman);
        middlemanField.CheckForNullOverride(Target.InteractionEventMiddleman, MessageBox, "Override middleman not set");

        if (!middlemanField.OverrideChecked && Target.gameObject.GetComponent<InteractionEventMiddleman>() == null)
        {
            MessageBox.AddMessage("No middleman on this GameObject", ErrorStyle);
            if (GUILayout.Button("Create middleman"))
                Target.gameObject.AddComponent<InteractionEventMiddleman>();
        }*/

    }
}
