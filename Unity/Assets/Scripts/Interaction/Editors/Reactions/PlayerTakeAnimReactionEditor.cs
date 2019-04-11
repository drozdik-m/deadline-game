using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerTakeAnimReaction))]
public class PlayerTakeAnimReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Player Take Animation Reaction";
    }
}
