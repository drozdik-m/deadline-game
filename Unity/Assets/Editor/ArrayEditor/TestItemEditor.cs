using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestItem))]
public class TestItemEditor : DefaultEditor<TestItem>
{
    public override void OnCustomInspectorGUI()
    {
        EditorGUILayout.LabelField("Hahaha");

        if (GUILayout.Button("Add game object as child"))
        {
            GameObject editorCollectionGO = GameObjectManager.Add(Target.gameObject, "_EditorCollection");
            GameObjectManager.Add(editorCollectionGO, "Item1");
            GameObject item2 = GameObjectManager.Add(editorCollectionGO, "Item2");

            GameObjectManager.Add(item2, "subItem2");

            GameObject[] children = GameObjectManager.GetChildren(editorCollectionGO);

            

            foreach (GameObject go in children)
            {
                Debug.Log(go.ToString());
            }

            if (GameObjectManager.TryFindChild(editorCollectionGO, "Item1", out GameObject foundGameObject))
            {
                Debug.Log("Found GameObject!!!" + foundGameObject.ToString());
                GameObjectManager.Remove(foundGameObject);
            }

            if (!GameObjectManager.TryFindChild(editorCollectionGO, "Item1", out GameObject foundGameObject2))
            {
                Debug.Log("It works");
            }

            Debug.Log(typeof(DebugReaction).ToString());
        }
    }
}
