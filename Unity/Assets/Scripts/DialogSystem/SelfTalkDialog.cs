using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Self-talk Dialog", menuName = "Dialog/Self-talk dialog")]
public class SelfTalkDialog : ScriptableObject
{
    public string characterName;

    [TextArea(2,30)]
    public string[] sentences;
}
