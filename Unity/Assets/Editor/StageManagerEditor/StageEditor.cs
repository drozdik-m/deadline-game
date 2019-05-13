using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class StageEditor : DefaultEditor<Stage>, IArrayItemEditor
    
{
    public abstract string GetFoldoutLabel();

    public override void OnCustomInspectorGUI()
    {
        OnStageInspectorGUI();

        //Ready
        if (Target.ReadyForNextStage())
            EditorGUILayout.LabelField("[Ready for next stage]", SuccessStyle);
    }

    public abstract void OnStageInspectorGUI();
}
