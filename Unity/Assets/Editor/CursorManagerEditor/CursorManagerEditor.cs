using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CursorManager))]
class CursorManagerEditor : DefaultEditor<CursorManager>
{
    public override void OnCustomInspectorGUI()
    {
        RequireTag("CursorManager");

        EditorGUILayout.LabelField("Textures should be 1:1 (square)");
        Target.NormalCursorTexture = EditorGUILayout.ObjectField("Normal cursor texture", Target.NormalCursorTexture, typeof(Texture2D), true) as Texture2D;
        Target.PointerCursorTexture = EditorGUILayout.ObjectField("Pointer cursor texture", Target.PointerCursorTexture, typeof(Texture2D), true) as Texture2D;

        if (Target.NormalCursorTexture == null || Target.PointerCursorTexture == null)
            MessageBox.AddMessage("One or more textures are null", ErrorStyle);

        Target.CursorMode = (CursorMode)EditorGUILayout.EnumPopup("Cursor mode", Target.CursorMode);
    }
}
