using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Single sound effect controller, that enables multiple instances of the same clip played.
/// </summary>
public class SoundEffectController : MonoBehaviour
{
    /// <summary>
    /// Audio sound clip
    /// </summary>
    public AudioClip UsedSoundEffect;

    /// <summary>
    /// Value from 0 to 1
    /// </summary>
    public float volume = 1f;

    /// <summary>
    /// Prefab for sound particle (Prefabs/Audio/SoundEffectParticle for regular particle)
    /// </summary>
    public GameObject SoundEffectParticlePrefab;

    private void Start()
    {
        

    }

    /// <summary>
    /// Plays the audio clip. It instantiates new gameobject so multiple sound effects can play in the same time.
    /// </summary>
    /// <param name="globalSound">If false, the new sound will be child of this gameobject. True for (0,0,0) in global world scope.</param>
    public void Play(bool globalSound = false)
    {
        GameObject spawnedSound;
        if (globalSound)
            spawnedSound = Instantiate(SoundEffectParticlePrefab);
        else
            spawnedSound = Instantiate(SoundEffectParticlePrefab, transform);

        AudioSource spawnedAudioSource = spawnedSound.GetComponent<AudioSource>();
        spawnedAudioSource.volume = volume;
        spawnedAudioSource.clip = UsedSoundEffect;
        spawnedAudioSource.Play();

    }

}
