using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BugResolverHandler(BugResolverSituation source, BugResolverArgs buggedObject);
/// <summary>
/// <see langword="abstract"/> Bug resolver situation, abstra
/// </summary>
public abstract class BugResolverSituation : MonoBehaviour
{
    /// <summary>
    /// Protect this object
    /// </summary>
    /// <returns>The protect.</returns>
    public abstract bool Protect();

}
