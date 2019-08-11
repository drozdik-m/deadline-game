using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor for No Item Stage Editor
/// </summary>
[CustomEditor(typeof(WaitAndGive))]
public class WaitAndGiveEditor : BuildStageEditor, IArrayItemEditor
{
    const string MAIN_INVENTORY_TAG = "MainInventory";

    OverrideMonoscriptField<Inventory> targetInventoryField =
        new OverrideMonoscriptField<Inventory>("Override Inventory", "Inventory");

    public override string GetFoldoutLabel()
    {
        return "WaitAndGive";
    }

    protected override void OnBuildStageInspectorGUI()
    {
        //DrawDefaultInspector();

        WaitAndGive thisBuildStage = Target as WaitAndGive;

        if (thisBuildStage.overrideInventory == null)
        {
            GameObject mainInventoryGameObject = GameObject.FindGameObjectWithTag(MAIN_INVENTORY_TAG);
            if (mainInventoryGameObject == null)
                MessageBox.AddMessage("Game Object with tag '" + MAIN_INVENTORY_TAG + "' was not found -> Add it", ErrorStyle);

            else if (mainInventoryGameObject.GetComponent<Inventory>() == null)
                MessageBox.AddMessage("Game Object with tag '" + MAIN_INVENTORY_TAG + "' does not have Inventory component-> Add it", ErrorStyle);
        }

        thisBuildStage.overrideInventory = targetInventoryField.Render(thisBuildStage.overrideInventory);
        targetInventoryField.CheckForNullOverride(thisBuildStage.overrideInventory,
                                                  MessageBox, "Inventory is overriden but empty",
                                                  WarningStyle);



        var delayProperty = new SerializedObject(target).FindProperty("Delay");
        var finishedProperty = new SerializedObject(target).FindProperty("CounterFinished");
        new SerializedObject(target).Update();
        EditorGUILayout.PropertyField(delayProperty, true);
        EditorGUILayout.PropertyField(finishedProperty, true);
        new SerializedObject(target).ApplyModifiedProperties();

       
                                                  
    }

}
