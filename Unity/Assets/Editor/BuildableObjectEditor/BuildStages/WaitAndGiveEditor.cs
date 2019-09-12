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

    private GameObject getParent() => Target.gameObject.transform.parent.gameObject.transform.parent.gameObject;

    private bool checkProviderPresence()
    {
        var parent = getParent();
        if (parent.GetComponent<ItemProvider>())
            return true;
        return false;
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
                                                  

        if (!checkProviderPresence())
        {
            if (GUILayout.Button("Add Item Provider"))
            {
                GameObject objectParent = getParent();
                objectParent.AddComponent<ItemProvider>();
                thisBuildStage.ItemProvider = objectParent.GetComponent<ItemProvider>();
            }

            MessageBox.AddMessage("Item Provider is empty", ErrorStyle);
        }
        else
        {
            thisBuildStage.ItemProvider = null;
        }


        thisBuildStage.Delay = EditorGUILayout.FloatField("Delay [sec]", thisBuildStage.Delay);


        if (thisBuildStage.CounterFinished)
        {
            MessageBox.AddMessage("[Transforming is finished]", SuccessStyle);
        }

    }

}
