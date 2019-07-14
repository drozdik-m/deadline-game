using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that contain all sound effects
/// </summary>
public class SoundEffectStorage : MonoBehaviour
{
    /// <summary>
    /// Struct that contain audio and type of the sound effect
    /// </summary>
    [System.Serializable]
    public struct SoundEffect
    {
        public AudioClip audio;
        public SoundEffectType type;
    }

    /// <summary>
    /// All used sound effect in the game
    /// </summary>
    public SoundEffect[] UsedSoundEffects;

    /// <summary>
    /// All sounds available through the type
    /// </summary>
    private Dictionary<SoundEffectType, AudioClip> allSoundEffects;

    // Start is called before the first frame update
    void Awake()
    {
        allSoundEffects = new Dictionary<SoundEffectType, AudioClip> ();

        foreach (var sound in UsedSoundEffects)
        {
            allSoundEffects.Add (sound.type, sound.audio);
        }
    }

    /// <summary>
    /// Get audio clip
    /// </summary>
    /// <param name="type">Type of the sound effect</param>
    /// <returns>Desired audio clip</returns>
    public AudioClip GetSoundEffect(SoundEffectType type)
    {
        AudioClip audio;

        allSoundEffects.TryGetValue (type, out audio);

        return audio;
    }

    /// <summary>
    /// Get array of the audio clips
    /// </summary>
    /// <param name="types">Array of the necessary types</param>
    /// <returns>Desired array of the audio clips</returns>
    public AudioClip[] GetSoundEffect(SoundEffectType[] types)
    {
        if (types.Length == 0)
            return null;

        AudioClip[] audio = new AudioClip [types.Length];
        for (int i = 0; i < types.Length; i++)
        {
            allSoundEffects.TryGetValue (types[i], out audio[i]);
        }
        
        return audio;
    }
}

/// <summary>
/// Types of the sound effects
/// </summary>
public enum SoundEffectType
{
    None,
    UIButtonClick,
    UIButtonHover,
    OpenMenu,
    FootstepFirst,
    FootstepSecond,
    FootstepThird,
    InteractButtonClick,
    InteractButtonHover,
    MaleVoiceNormal,
    MaleVoiceSurprised,
    QuestCompleted,
    QuestStackCompleted
}
