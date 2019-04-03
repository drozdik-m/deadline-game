using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Condition object represents specific condition.
/// </summary>
public class Condition : ScriptableObject
{
    /// <summary>
    /// specific description of condition
    /// </summary>
    public string description;

    /// <summary>
    /// indicates whether the condition is satisfied
    /// </summary>
    public bool satisfied;

    /// <summary>
    /// hash used for checking if two conditions are equal
    /// </summary>
    public int hash;
}
