using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CustomEditor(typeof(ScenesChangeManager))]
public class ScenesChangeManagerEditor : DefaultEditor<ScenesChangeManager>
{
    public override void OnCustomInspectorGUI()
    {
        //Tag
        RequireTag("InvincibleObject");
    }
}
