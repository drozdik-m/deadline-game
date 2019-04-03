using UnityEngine;
using UnityEditor;

/// <summary>
/// custom editor for debug reaction
/// </summary>
[CustomEditor(typeof(DebugReaction))]
public class DebugReactionEditor : ReactionEditor
{
    private SerializedProperty debugMessageProperty;

    private const string debugReactionPropDebugMessageName = "debugMessage";
    private const float areaWidthOfSet = 19f;
    private const float debugMessageGUILines = 3f;

    protected override void Init()
    {
        debugMessageProperty = serializedObject.FindProperty(debugReactionPropDebugMessageName);
    }

    protected override void DrawReaction()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Message", GUILayout.Width(EditorGUIUtility.labelWidth - areaWidthOfSet));
        debugMessageProperty.stringValue =
            EditorGUILayout.TextArea(debugMessageProperty.stringValue,
                GUILayout.Height(EditorGUIUtility.singleLineHeight * debugMessageGUILines));

        EditorGUILayout.EndHorizontal();

        // easy way to do property fields
        //EditorGUILayout.PropertyField(debugMessageProperty);
    }

    protected override string GetFoldoutLabel()
    {
        return "Debug Reaction";
    }
}
