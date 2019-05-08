using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
    
/// <summary>
/// Interface requiring fold out name for array item editor
/// </summary>
public interface IArrayItemEditor
{
    /// <summary>
    /// Get foldout label for given target that editor creates
    /// </summary>
    /// <returns></returns>
    string GetFoldoutLabel();
}
