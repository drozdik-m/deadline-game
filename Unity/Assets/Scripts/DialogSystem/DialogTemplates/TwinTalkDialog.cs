using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Twin-talk Dialog", menuName = "Dialog/Twin-talk dialog")]
public class TwinTalkDialog : ScriptableObject
{
 

    [System.Serializable]
    public class SentenceStructure
    {
        public enum CharacterIdentifier
        {
            A,
            B
        }
        public CharacterIdentifier CharacterID;
        [TextArea(1, 25)]
        public string sentence;
    }

    public SentenceStructure[] structuredSentences;


}
