using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class TabSoundSettings : Tab
{
    public Slider MasterVolumeSlider;
    public Slider BackgroundVolumeSlider;
    public Slider SoundEffectsVolumeSlider;

    public AudioMixer AudioMixerMaster;

    public override void SaveData()
    {
        base.SaveData();
        PlayerPrefs.SetFloat("MasterVolume", MasterVolumeSlider.value);
        PlayerPrefs.SetFloat("BackgroundVolume", BackgroundVolumeSlider.value);
        PlayerPrefs.SetFloat("SoundEffectsVolume", SoundEffectsVolumeSlider.value);
    }

    public override void LoadData()
    {
        base.LoadData();
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        BackgroundVolumeSlider.value = PlayerPrefs.GetFloat("BackgroundVolume");
        SoundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume");

        SetMasterVolume();
        SetBackgroundVolume();
        SetSoundEffectsVolume();
    }

    /// <summary>
    /// Change the master volume of the game
    /// </summary>
    public void SetMasterVolume()
    {
        AudioMixerMaster.SetFloat("ExposedMasterVolume", MasterVolumeSlider.value);
    }

    /// <summary>
    /// Change the background volume of the game
    /// </summary>
    public void SetBackgroundVolume()
    {
        AudioMixerMaster.SetFloat("ExposedBackgroundVolume", BackgroundVolumeSlider.value);
    }

    /// <summary>
    /// Change the volume of the game
    /// </summary>
    public void SetSoundEffectsVolume()
    {
        AudioMixerMaster.SetFloat("ExposedSoundEffectsVolume", SoundEffectsVolumeSlider.value);
    }
}
