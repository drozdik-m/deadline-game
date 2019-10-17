using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ClassicInventoryItem))]
class ClassicInventoryItemEditor : InventoryItemEditor<ClassicInventoryItem>
{
    OverrideMonoscriptField<GameObject> pickableObjectInSceneInput
        = new OverrideMonoscriptField<GameObject>("Pickable GameObject is not first child", "Pickable GameObject");

    OverrideMonoscriptField<GameObject> dropTargetInput
        = new OverrideMonoscriptField<GameObject>("Don't use recommended drop target", "Drop target");

    protected void CreateClassicInventoryItemEditor()
    {
        //Call parent editor
        CreateInventoryItemEditor();

        //Pickable GameObject
        Target.PickableObjectInScene = pickableObjectInSceneInput.Render(Target.PickableObjectInScene);
        if (Target.PickableObjectInScene == null && Target.transform.childCount == 0 
            && !pickableObjectInSceneInput.OverrideChecked)
        {
            MessageBox.AddMessage("No children for default pickable GameObject", ErrorStyle);
            if (GUILayout.Button("Create child for pickable GameObject"))
                GameObjectManager.Add(Target.gameObject, "[Inventory item]");
        }
        pickableObjectInSceneInput.CheckForNullOverride(Target.PickableObjectInScene, MessageBox, "Pickable object not set", ErrorStyle);

        //Drop target
        Target.DropTarget = dropTargetInput.Render(Target.DropTarget);
        dropTargetInput.CheckForNullOverride(Target.DropTarget, MessageBox, "Drop target not set", ErrorStyle);
    }

    public override void OnCustomInspectorGUI()
    {
        CreateClassicInventoryItemEditor();
    }
}
