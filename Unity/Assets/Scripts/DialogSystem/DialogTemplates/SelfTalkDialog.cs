using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Self talk dialog.
/// </summary>
[CreateAssetMenu(fileName ="New Self-talk Dialog", menuName = "Dialog/Self-talk dialog")]
public class SelfTalkDialog : ScriptableObject
{
    /// <summary>
    /// The sentences array of the dialog.
    /// </summary>
    [TextArea(1,25)]
    public string[] sentences;
}
