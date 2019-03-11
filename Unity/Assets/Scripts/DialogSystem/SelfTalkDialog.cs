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
    /// The name of the character.
    /// </summary>
    public string characterName;
    /// <summary>
    /// The sentences array of the dialog.
    /// </summary>
    [TextArea(2,30)]
    public string[] sentences;
}
