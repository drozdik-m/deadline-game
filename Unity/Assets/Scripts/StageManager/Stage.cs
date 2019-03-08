using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for StageManager. Stage represents stage of some scene. Used for story.
/// </summary>
public abstract class Stage : MonoBehaviour
{
    /// <summary>
    /// Called on stage load
    /// </summary>
    public abstract void StageLoad();

    /// <summary>
    /// Called on every frame
    /// </summary>
    public abstract void StageUpdate();

    /// <summary>
    /// Updated fast (like frame) but with fixed time between. Used for physics mainly. 
    /// </summary>
    public abstract void StageFixedUpdate();

    /// <summary>
    /// Called on stage end
    /// </summary>
    public abstract void StageEnd();

    /// <summary>
    /// Is this stage ready for the next stage? StageManager will change stages on the next frame.
    /// </summary>
    /// <returns>True if ready for this stage to end</returns>
    public abstract bool ReadyForNextStage();
}
