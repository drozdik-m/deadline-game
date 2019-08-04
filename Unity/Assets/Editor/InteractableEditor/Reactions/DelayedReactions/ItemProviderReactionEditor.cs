using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom Editor for Item Provider Reaction
/// </summary>
[CustomEditor(typeof(ItemProviderReaction))]
public class ItemProviderReactionEditor : ReactionEditor
{
    public override string GetFoldoutLabel()
    {
        return "Item Provider Reaction";
    }

    public override void OnCustomInspectorGUI()
    {
        ItemProviderReaction thisReaction = Target as ItemProviderReaction;

        thisReaction.delay = EditorGUILayout.Slider("Delay", thisReaction.delay, 0, 5);

        thisReaction.itemProvider = (ItemProvider)EditorGUILayout
            .ObjectField("Item Provider",
                         thisReaction.itemProvider,
                         typeof(ItemProvider),
                         true);

        if (thisReaction.itemProvider == null)
            MessageBox.AddMessage("Item Provider is empty", WarningStyle);
    }
}
