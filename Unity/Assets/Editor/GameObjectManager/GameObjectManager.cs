using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// GameObjectManager enables manipulation with gameobjects in hierarchy
/// </summary>
public static class GameObjectManager
{
    /// <summary>
    /// Adds new empty GameObject
    /// </summary>
    /// <param name="parent">Parent of the new GameObject</param>
    /// <param name="name">Name of the new GameObject</param>
    /// <returns></returns>
    public static GameObject Add(GameObject parent, string name)
    {
        GameObject newGameObject = new GameObject(name);
        newGameObject.transform.parent = parent.gameObject.transform;
        return newGameObject;
    }

    /// <summary>
    /// Removes GameObject
    /// </summary>
    /// <param name="gameObject">GameObject to be removed</param>
    public static void Remove(GameObject gameObject)
    {
        Object.DestroyImmediate(gameObject);
    }

    /// <summary>
    /// Gets children of the GameObject
    /// </summary>
    /// <param name="parent">Parent of which the children will be returned</param>
    /// <returns></returns>
    public static GameObject[] GetChildren(GameObject parent)
    {
        List<GameObject> gameObjects = new List<GameObject>();
        foreach (Transform child in parent.transform)
            gameObjects.Add(child.gameObject);

        return gameObjects.ToArray();
    }

    /// <summary>
    /// Tries to find child with specific name
    /// </summary>
    /// <param name="parent">Parent whose children will be searched</param>
    /// <param name="name">Name of the searched child</param>
    /// <param name="foundGameObject">Found child</param>
    /// <returns>True if the object is found, false if not</returns>
    public static bool TryFindChild(GameObject parent, string name, out GameObject foundGameObject)
    {
        GameObject[] children = GetChildren(parent);

        foreach (GameObject go in children)
        {
            if (go.name == name)
            {
                foundGameObject = go;
                return true;
            }
        }

        foundGameObject = null;
        return false;
    }

    /// <summary>
    /// Sorts children of the given parent aplhabetically
    /// </summary>
    /// <param name="parent">Gameobject parent whose children will be sorted</param>
    public static void SortChildrenAlphabetically(GameObject parent)
    {
        GameObject[] children = GetChildren(parent);

        children = children.OrderBy(c => c.name).ToArray();

        for (int i = 0; i < children.Length; i++)
            children[i].transform.SetSiblingIndex(i);
    }
}
