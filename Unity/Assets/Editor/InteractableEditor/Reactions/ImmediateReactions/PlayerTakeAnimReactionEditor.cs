using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerTakeAnimReaction))]
public class PlayerTakeAnimReactionEditor : ReactionEditor
{
    public override string GetFoldoutLabel()
    {
        return "Player Take Anim Reaction";
    }

    public override void OnCustomInspectorGUI()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            MessageBox.AddMessage("There must be GameObject with tag Player in the scene", WarningStyle);
    }
}
