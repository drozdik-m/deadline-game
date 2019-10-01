
using UnityEngine;
/// <summary>
/// Bug resolver arguments.
/// </summary>
public class BugResolverArgs
{
    /// <summary>
    /// Gets the bugged item game object.
    /// </summary>
    /// <value>The bugged item game object.</value>
    public GameObject BuggedItemGameObject
    {
        get
        {
            return buggedItemGameObject;
        }
    }

    /// <summary>
    /// The bugged item game object.
    /// </summary>
    private readonly GameObject buggedItemGameObject;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:BugResolverArgs"/> class.
    /// </summary>
    /// <param name="buggedItemGameObject">Bugged item game object.</param>
    /// <param name="playerWasBugged">If set to <c>true</c> player was bugged.</param>
    public BugResolverArgs(GameObject buggedItemGameObject )
    {
        this.buggedItemGameObject = buggedItemGameObject;
    }
}
