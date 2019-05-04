using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ReactionCollection))]
public class ReactionCollectionEditor : DefaultEditor<ReactionCollection>
{
    public override void OnCustomInspectorGUI()
    {
        ArrayEditor<ReactionCollection, Reaction, ReactionEditor> arrEditor
            = new ArrayEditor<ReactionCollection, Reaction, ReactionEditor>("myReactionCollection");

        arrEditor.Use(Target);
    }
}
