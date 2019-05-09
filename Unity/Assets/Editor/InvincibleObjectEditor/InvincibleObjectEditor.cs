using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CustomEditor(typeof(InvincibleObject))]
public class InvincibleObjectEditor : DefaultEditor<InvincibleObject>
{
    public override void OnCustomInspectorGUI()
    {
        //Tag
        RequireTag("InvincibleObject");
    }
}
