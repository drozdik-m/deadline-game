using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// custom editor for condition reaction
/// </summary>
[CustomEditor(typeof(ConditionReaction))]
public class ConditionReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Condition Reaction";
    }
}
