using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor for Reaction Collection
/// </summary>
[CustomEditor(typeof(ReactionCollection))]
public class ReactionCollectionEditor : DefaultEditor<ReactionCollection>
{
    ArrayEditor<ReactionCollection, Reaction, ReactionEditor> arrEditor;

    public ReactionCollectionEditor()
    {
        arrEditor = new ArrayEditor<ReactionCollection, Reaction, ReactionEditor>("myReactionCollection", MessageBox);
    }
     
    public override void OnCustomInspectorGUI()
    {
        EditorGUILayout.LabelField("Reaction Collection");
        Target.reactions = arrEditor.Use(Target);
    }
}
