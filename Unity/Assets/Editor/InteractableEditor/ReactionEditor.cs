using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Abstract Custom Editor for Reaction
/// </summary>
public abstract class ReactionEditor : DefaultEditor<Reaction>, IArrayItemEditor
{
    public abstract string GetFoldoutLabel();

    public override void OnCustomInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
