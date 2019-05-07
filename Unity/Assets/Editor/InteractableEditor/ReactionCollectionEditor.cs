using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
        Target.reactions = arrEditor.Use(Target);
       // Debug.Log(reactions.Length);
    }
}
