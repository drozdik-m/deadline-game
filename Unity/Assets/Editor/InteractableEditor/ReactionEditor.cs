using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class ReactionEditor : DefaultEditor<Reaction>, IArrayItemEditor
{
    public abstract string GetFoldoutLabel();

    public override void OnCustomInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
