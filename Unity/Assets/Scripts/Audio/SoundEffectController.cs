using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


/// <summary>
/// Single sound effect controller, that enables multiple instances of the same clip played.
/// </summary>
public class SoundEffectController : MonoBehaviour
{
    /// <summary>
    /// Audio mixer 
    /// </summary>
    public AudioMixer AudioMixerMaster;

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
    /// Audio sound effects
    /// </summary>
    public SoundEffect[] UsedSoundEffects;

    /// <summary>
    /// Sound volume, value from 0 to 1
    /// </summary>
    private float volume;

    /// <summary>
    /// Prefab for sound particle (Prefabs/Audio/SoundEffectParticle for regular particle)
    /// </summary>
    public GameObject SoundEffectParticlePrefab;

    /// <summary>
    /// Contain all the audio clips available using the type of the sound effect
    /// </summary>
    private Dictionary<SoundEffectType, AudioClip> soundEffectsStorage;

    private void Start()
    {   
        float masterVolume, soundEffectsVolume;
        AudioMixerMaster.GetFloat ("ExposedMasterVolume", out masterVolume);
        AudioMixerMaster.GetFloat ("ExposedSoundEffectsVolume", out soundEffectsVolume);

        // Multiply master volume and sound effects volume to get used volume
        volume = ( ( 80 + masterVolume ) * ( 80 + soundEffectsVolume ) ) / ( 6400 );

        soundEffectsStorage = new Dictionary<SoundEffectType, AudioClip> ();
        foreach (SoundEffect soundEffect in UsedSoundEffects)
        {
            soundEffectsStorage.Add (soundEffect.type, soundEffect.audio);
        }
    }

    /// <summary>
    /// Plays the audio clip. It instantiates new gameobject so multiple sound effects can play in the same time
    /// </summary>
    /// <param name="type">Type of the sound effet to play</param>
    /// <param name="globalSound">If false, the new sound will be child of this gameobject. True for (0,0,0) in global world scope</param>
    public void PlaySound(SoundEffectType type, bool globalSound = false)
    {
        AudioClip audio;
        soundEffectsStorage.TryGetValue (type, out audio);

        if (audio != null)
        {
            GameObject spawnedSound;
            if (globalSound)
                spawnedSound = Instantiate (SoundEffectParticlePrefab);
            else
                spawnedSound = Instantiate (SoundEffectParticlePrefab, transform);

            AudioSource spawnedAudioSource = spawnedSound.GetComponent<AudioSource> ();
            spawnedAudioSource.volume = volume;
            spawnedAudioSource.clip = audio;
            spawnedAudioSource.Play ();
        }
    }
}
/// <summary>
/// Types of the sound effect. Numbers in type name mean duration
/// </summary>
public enum SoundEffectType
{
    UIButtonClick,
    UIButtonHover,
    OpenMenu,
    InteractableItemHover,
    InteractableItemClick,
    QuestCompleted,
    QuestStackCompleted,
    MaleVoiceNormal,
    MaleVoiceSurprised,
    FootstepFirst,
    FootstepSecond,
    FootstepThird,
}

