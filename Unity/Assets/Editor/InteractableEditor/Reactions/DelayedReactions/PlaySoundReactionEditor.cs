using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlaySoundReaction))]
public class PlaySoundReactionEditor : ReactionEditor
{
    public override string GetFoldoutLabel()
    {
        return "Play Sound Reaction";
    }

    public override void OnCustomInspectorGUI()
    {
        PlaySoundReaction thisReaction = Target as PlaySoundReaction;

        thisReaction.delay = EditorGUILayout.Slider("Delay", thisReaction.delay, 0, 5);
        thisReaction.soundEffectController = (SoundEffectController)EditorGUILayout
            .ObjectField("Sound Effect Controller",
                         thisReaction.soundEffectController,
                         typeof(SoundEffectController),
                         true);

        if (thisReaction.soundEffectController == null)
            MessageBox.AddMessage("Sound Effect Controller is empty", WarningStyle);
    }
}
