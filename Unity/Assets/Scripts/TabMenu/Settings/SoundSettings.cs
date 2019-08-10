using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider MasterVolumeSlider;
    public Slider BackgroundVolumeSlider;
    public Slider SoundEffectsVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        bool load = Convert.ToBoolean( PlayerPrefs.GetInt("Saved") );

        if (load)
        {
            LoadData();
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterVolumeSlider.value);
        PlayerPrefs.SetFloat("BackgroundVolume", BackgroundVolumeSlider.value);
        PlayerPrefs.SetFloat("SoundEffectsVolume", SoundEffectsVolumeSlider.value);
        PlayerPrefs.SetInt("Saved", 1);
    }

    void LoadData()
    {
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        BackgroundVolumeSlider.value = PlayerPrefs.GetFloat("BackgroundVolume");
        SoundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume");
    }
}
