using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TwinDialogReaction))]
public class TwinDialogReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Twin Dialog Reaction";
    }
}
