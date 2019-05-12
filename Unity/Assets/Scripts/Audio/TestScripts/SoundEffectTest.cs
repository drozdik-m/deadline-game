using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectTest : MonoBehaviour
{
    public SoundEffectController controller;

    public SoundEffectType type;

    public void Play()
    {
        controller.PlaySound (type);
    }

    public void PlayButtonClick()
    {
        controller.PlaySound (SoundEffectType.UIButtonClick);
    }

    public void PlayMaleVoice()
    {
        controller.PlaySound (SoundEffectType.MaleVoiceNormal);
    }

    public void PlayQuestCompl()
    {
        controller.PlaySound (SoundEffectType.QuestCompleted);
    }

    public void PlayQuestStackCompl()
    {
        controller.PlaySound (SoundEffectType.QuestStackCompleted);
    }
}
