using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConditionReaction))]
public class ConditionReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Condition Reaction";
    }
}
