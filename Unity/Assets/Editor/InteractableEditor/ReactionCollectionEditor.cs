using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ReactionCollection))]
public class ReactionCollectionEditor : DefaultEditor<ReactionCollection>
{
    public override void OnCustomInspectorGUI()
    {
        Debug.Log("Reaction collection On Custom Inspector GUI");

        ArrayEditor<ReactionCollection, Reaction, ReactionEditor>.CreateArrayEditor(Target);
    }
}
