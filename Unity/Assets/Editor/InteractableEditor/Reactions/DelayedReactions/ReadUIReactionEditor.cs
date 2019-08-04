using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ReadUIReaction))]
public class ReadUIReactionEditor : ReactionEditor
{
    public override string GetFoldoutLabel()
    {
        return "Read UI Reaction";
    }

    public override void OnCustomInspectorGUI()
    {
        ReadUIReaction thisReaction = Target as ReadUIReaction;

        thisReaction.delay = EditorGUILayout.Slider("Delay", thisReaction.delay, 0, 5);
        thisReaction.readableObjectUI = (ReadableObjectUI)EditorGUILayout
            .ObjectField("Readable Object UI",
                         thisReaction.readableObjectUI,
                         typeof(ReadableObjectUI),
                         true);

        if (thisReaction.readableObjectUI == null)
            MessageBox.AddMessage("Readable Object UI is empty", WarningStyle);
    }
}
