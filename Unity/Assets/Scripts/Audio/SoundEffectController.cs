﻿using System.Collections;
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
    /// Current sound effect
    /// </summary>
    public AudioClip UsedSoundEffect;

    /// <summary>
    /// Sound volume, value from 0 to 1
    /// </summary>
    private float volume;

    /// <summary>
    /// Loop sound effect
    /// </summary>
    private bool loop;

    /// <summary>
    /// Prefab for sound particle (Prefabs/Audio/SoundEffectParticle for regular particle)
    /// </summary>
    public GameObject SoundEffectParticlePrefab;

    private void Start()
    {
        UpdateVolume ();
    }

    /// <summary>
    /// Plays the audio clip. It instantiates new gameobject so multiple sound effects can play in the same time
    /// </summary>
    /// <param name="parent">The new sound will be child of the parent</param>
    public void PlaySound(Transform parent)
    {
        UpdateVolume ();
        GameObject spawnedSound = Instantiate (SoundEffectParticlePrefab, parent);

        AudioSource spawnedAudioSource = spawnedSound.GetComponent<AudioSource> ();
        spawnedAudioSource.volume = volume;
        spawnedAudioSource.clip = UsedSoundEffect;
        spawnedAudioSource.Play ();
    }

    /// <summary>
    /// Plays the audio clip. It instantiates new gameobject so multiple sound effects can play in the same time
    /// </summary>
    public void PlaySound()
    {
        UpdateVolume ();
        GameObject spawnedSound = Instantiate (SoundEffectParticlePrefab);

        AudioSource spawnedAudioSource = spawnedSound.GetComponent<AudioSource> ();
        spawnedAudioSource.volume = volume;
        spawnedAudioSource.clip = UsedSoundEffect;
        spawnedAudioSource.Play ();
    }

    /// <summary>
    /// Plays the audio clip in the loop. It instantiates new gameobject so multiple sound effects can play in the same time
    /// </summary>
    public void PlayLoopSound(float delay)
    {
        StartCoroutine (loopSound (delay));
    }

    /// <summary>
    /// Stops the audio clip in the loop.
    /// </summary>
    public void StopLoopSound()
    {
        loop = false; 
    }

    /// <summary>
    /// Plays sound effect every time
    /// </summary>
    /// <param name="delay">Delay between each play</param>
    /// <returns></returns>
    private IEnumerator loopSound(float delay)
    {
        loop = true;
        while (loop)
        {
            PlaySound ();
            yield return new WaitForSeconds (delay);
        }
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
        volume = ( ( 80 + masterVolume ) * ( 80 + soundEffectsVolume ) ) / ( 6400 );
    }
}