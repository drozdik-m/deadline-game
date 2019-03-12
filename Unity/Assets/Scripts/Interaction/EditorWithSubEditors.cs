using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public abstract class EditorWithSubEditors<TEditor, TTarget> : Editor
    where TEditor : Editor
    where TTarget : UnityEngine.Object
{
    protected TEditor[] subEditors;

    protected void CheckAndCreateSubEditors(TTarget[] subEditorTargets)
    {
        if (subEditors != null && subEditors.Length == subEditorTargets.Length)
            return;

        CleanupEditors();

        subEditors = new TEditor[subEditorTargets.Length];

        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i] = CreateEditor(subEditorTargets[i]) as TEditor;
            SubEditorSetup(subEditors[i]);
        }
    }

    protected void CleanupEditors()
    {
        if (subEditors == null) return;

        for (int i = 0; i < subEditors.Length; i++)
            DestroyImmediate(subEditors[i]);

        subEditors = null;
    }

    protected abstract void SubEditorSetup(TEditor editor);
}
