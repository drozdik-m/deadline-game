using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CustomEditor(typeof(WorkflowScene))]
public class WorkflowSceneEditor : DefaultEditor<WorkflowScene>, IArrayItemEditor
{
    OverrideMonoscriptField<StageManager> stageManagerField = new OverrideMonoscriptField<StageManager>("Override main stage manager");

    public string GetFoldoutLabel()
    {
        return "Scene";
    }

    public override void OnCustomInspectorGUI()
    {
        //Scene name
        Target.SceneName = EditorGUILayout.TextField("Scene file name", Target.SceneName);
        if (!Application.CanStreamedLevelBeLoaded(Target.SceneName))
        {
            MessageBox.AddMessage("Scene " + Target.SceneName + " is currently not loadable.", WarningStyle);
            MessageBox.AddMessage("--> Is it included in the build?", WarningStyle);
        }

        //Main StageManager
        Target.MainStageManger = stageManagerField.Render(Target.MainStageManger);
        stageManagerField.CheckForNullOverride(Target.MainStageManger, MessageBox, "Override StageManager is not set");
        if (Target.MainStageManger == null && !stageManagerField.OverrideChecked)
        {
            if (GameObject.FindGameObjectWithTag("MainStageManager") == null)
            {
                MessageBox.AddMessage("MainStageManager not found", ErrorStyle);
                if (GUILayout.Button("Create MainStageManger"))
                {
                    GameObject msm = GameObjectManager.Add(null, "-------MAIN STAGE MANAGER-------");
                    msm.tag = "MainStageManager";
                    msm.AddComponent<StageManager>();
                }
            }
            else if (GameObject.FindGameObjectWithTag("MainStageManager").GetComponent<StageManager>() == null)
                MessageBox.AddMessage("MainStageManager does not have StageManagerComponent", ErrorStyle);

        }

        //Stage music theme
        Target.StageMusicTheme = (BackgroundMusicTheme)EditorGUILayout.EnumPopup("Background music", Target.StageMusicTheme);

        //Next scene transition color
        Target.NextSceneTransitionColor = EditorGUILayout.ColorField("Transition color", Target.NextSceneTransitionColor);

        //Transition speed
        Target.NextSceneTransitionSpeed = EditorGUILayout.FloatField("Transition speed", Target.NextSceneTransitionSpeed);

    }
}
