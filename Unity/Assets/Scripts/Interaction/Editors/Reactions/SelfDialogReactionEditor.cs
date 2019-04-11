using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SelfDialogReaction))]
public class SelfDialogReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Self Dialog Reaction";
    }
}
