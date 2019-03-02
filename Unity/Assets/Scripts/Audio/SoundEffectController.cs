using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    public AudioClip UsedSoundEffect;
    public float volume = 1f;
    public GameObject SoundEffectParticle;

    private void Start()
    {
        

    }

    public void Play(bool globalSound = false)
    {
        GameObject spawnedSound = (GameObject)Instantiate(SoundEffectParticle);
        spawnedSound.GetComponent<AudioSource>().volume = volume;

        if (globalSound)
            spawnedSound.transform.parent = transform;
    }

}
