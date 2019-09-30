
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
    /// Gets a value indicating whether this <see cref="T:BugResolverArgs"/> player was bugged.
    /// </summary>
    /// <value><c>true</c> if player was bugged; otherwise, <c>false</c>.</value>
    public bool PlayerWasBugged
    {
        get
        {
            return playerWasBugged;
        }
    }
    /// <summary>
    /// The bugged item game object.
    /// </summary>
    private readonly GameObject buggedItemGameObject;
    /// <summary>
    /// True, if the player got bugged.
    /// </summary>
    private readonly bool playerWasBugged;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:BugResolverArgs"/> class.
    /// </summary>
    /// <param name="buggedItemGameObject">Bugged item game object.</param>
    /// <param name="playerWasBugged">If set to <c>true</c> player was bugged.</param>
    public BugResolverArgs(GameObject buggedItemGameObject, bool playerWasBugged )
    {
        this.buggedItemGameObject = buggedItemGameObject;
        this.playerWasBugged = playerWasBugged;
    }
}
