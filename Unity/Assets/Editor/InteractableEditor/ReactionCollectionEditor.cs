using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ReactionCollection))]
public class ReactionCollectionEditor : DefaultEditor<ReactionCollection>
{

    ArrayEditor<ReactionCollection, Reaction, ReactionEditor> arrEditor
            = new ArrayEditor<ReactionCollection, Reaction, ReactionEditor>("myReactionCollection");

    public override void OnCustomInspectorGUI()
    {
        arrEditor.Use(Target);
    }
}
