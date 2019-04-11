using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom editor for adding item to inventory reaction
/// </summary>
[CustomEditor(typeof(InventoryItemReaction))]
public class InventoryItemReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Add to inventory reaction";
    }
}
