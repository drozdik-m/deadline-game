using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom editor for dialog reaction
/// </summary>
[CustomEditor(typeof(DialogReaction))]
public class DialogReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Dialog Reaction";
    }
}
