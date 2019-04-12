using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildableReaction))]
public class BuildableReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Buildable Reaction";
    }
}
