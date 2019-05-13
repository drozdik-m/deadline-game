using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class QuestConditionEditor : DefaultEditor<QuestCondition>, IArrayItemEditor
    
{
    public abstract string GetFoldoutLabel();

    public override void OnCustomInspectorGUI()
    {
        OnConditionInspectorGUI();

        //Condition met
        if (Target.ConditionMet())
            EditorGUILayout.LabelField("[Condition met]", SuccessStyle);
        if (Target.Recording)
            EditorGUILayout.LabelField("[Recording]", NormalStyle);
    }

    public abstract void OnConditionInspectorGUI();
}
