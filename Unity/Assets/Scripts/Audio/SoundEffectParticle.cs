using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sound effect particle. It dies after sound effect end. 
/// </summary>
public class SoundEffectParticle : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
            Destroy(gameObject);
        Debug.Log(audioSource.isPlaying);
    }
}
