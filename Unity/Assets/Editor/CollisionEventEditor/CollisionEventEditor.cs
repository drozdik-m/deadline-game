using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CollisionEvent))]
class CollisionEventEditor : DefaultEditor<CollisionEvent>
{
   

    public override void OnCustomInspectorGUI()
    {
        if (Target.GetComponent<Collider>() == null)
            MessageBox.AddMessage("Must be placed with a collider component!", ErrorStyle);
    }
}
