using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Twin-talk Dialog", menuName = "Dialog/Twin-talk dialog")]
public class TwinTalkDialog : ScriptableObject
{
    [System.Serializable]
    public class SentenceStructure
    {
        public int characterId;
        [TextArea(2, 30)]
        public string sentence;
    }

    public SentenceStructure[] structuredSentences;


}
