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
    public AudioMixer AudioMixer;

    /// <summary>
    /// Used type of the sound effect
    /// </summary>
    public SoundEffectType SingleSoundEffectType;

    /// <summary>
    /// Current sound effect
    /// </summary>
    private AudioClip soundEffect;

    /// <summary>
    /// Sound volume, value from 0 to 1
    /// </summary>
    private float volume;

    /// <summary>
    /// Prefab for sound particle (Prefabs/Audio/SoundEffectParticle for regular particle)
    /// </summary>
    public GameObject SoundEffectParticlePrefab;

    /// <summary>
    /// The sound effects storage
    /// </summary>
    private SoundEffectStorage soundEffectsStorage;
    /// <summary>
    /// The spawned audio source.
    /// </summary>
    private AudioSource spawnedAudioSource;

    private void Start()
    {
        soundEffectsStorage = GameObject.FindGameObjectWithTag ("SoundEffectStorage").GetComponent<SoundEffectStorage> ();
        if (SingleSoundEffectType != SoundEffectType.None)
            soundEffect = soundEffectsStorage.GetSoundEffect (SingleSoundEffectType);
        UpdateVolume ();
    }

    /// <summary>
    /// Plays the audio clip. It instantiates new gameobject so multiple sound effects can play in the same time
    /// </summary>
    /// <param name="parent">The new sound will be child of the parent</param>
    public void PlaySound(Transform parent)
    {
        UpdateVolume ();
        var spawnedSound = Instantiate (SoundEffectParticlePrefab, parent);

        spawnedAudioSource = spawnedSound.GetComponent<AudioSource> ();
        spawnedAudioSource.volume = volume;
        spawnedAudioSource.clip = soundEffect;
        spawnedAudioSource.Play ();
    }

    /// <summary>
    /// Plays the audio clip. It instantiates new gameobject so multiple sound effects can play in the same time
    /// </summary>
    public void PlaySound()
    {
        UpdateVolume ();
        var spawnedSound = Instantiate (SoundEffectParticlePrefab);

        spawnedAudioSource = spawnedSound.GetComponent<AudioSource> ();
        spawnedAudioSource.volume = volume;
        spawnedAudioSource.clip = soundEffect;
        spawnedAudioSource.Play ();

        //Debug.Log("Sound play - " + spawnedSound);
    }

    public void StopLastSound()
    {
        if (spawnedAudioSource != null)
            Destroy(spawnedAudioSource.gameObject);
    }

    /// <summary>
    /// Updates sound effect volume
    /// </summary>
    public void UpdateVolume()
    {
        float masterVolume, soundEffectsVolume;
        AudioMixer.GetFloat ("ExposedMasterVolume", out masterVolume);
        AudioMixer.GetFloat ("ExposedSoundEffectsVolume", out soundEffectsVolume);

        // Multiply master volume and sound effects volume to get used volume
        volume = ( ( 40 + masterVolume ) * ( 40 + soundEffectsVolume ) ) / ( 1600 );
    }
}
