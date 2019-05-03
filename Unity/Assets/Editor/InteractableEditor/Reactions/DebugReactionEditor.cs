using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugReactionEditor : ReactionEditor
{
    public override void OnArrayItemInspectorGUI()
    {
        Debug.Log("On Array Item Inspector GUI: DebugReactionEditor");
    }
}
