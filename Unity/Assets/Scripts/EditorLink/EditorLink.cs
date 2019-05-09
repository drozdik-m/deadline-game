using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Link for game objects (used as array item)
/// </summary>
public class EditorLink : MonoBehaviour
{
    /// <summary>
    /// Game Object to which editor link links
    /// </summary> 
    public GameObject linkedGameObject;

    public static string FoldoutName = "Editor Link";
}
