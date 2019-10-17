using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

/// <summary>
/// Class represents sound settings
/// </summary>
public class TabSoundSettings : Tab
{
    /// <summary>
    /// Slider controls Master volume
    /// </summary>
    public Slider MasterVolumeSlider;

    /// <summary>
    /// Slider controls Background volume
    /// </summary>
    public Slider BackgroundVolumeSlider;

    /// <summary>
    /// Slider controls Sound effects volume
    /// </summary>
    public Slider SoundEffectsVolumeSlider;

    /// <summary>
    /// Audio mixer
    /// </summary>
    public AudioMixer AudioMixerMaster;

    /// <summary>
    /// Saves graphics settings to PlayerPrefs
    /// </summary>
    public override void SaveData()
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterVolumeSlider.value);
        PlayerPrefs.SetFloat("BackgroundVolume", BackgroundVolumeSlider.value);
        PlayerPrefs.SetFloat("SoundEffectsVolume", SoundEffectsVolumeSlider.value);
    }

    /// <summary>
    /// Loads graphics settings from PlayerPrefs
    /// </summary>
    public override void LoadData()
    {
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
        if (MasterVolumeSlider.value == MasterVolumeSlider.minValue)
            AudioMixerMaster.SetFloat("ExposedMasterVolume", -80);
        else
            AudioMixerMaster.SetFloat("ExposedMasterVolume", MasterVolumeSlider.value);
    }

    /// <summary>
    /// Change the background volume of the game
    /// </summary>
    public void SetBackgroundVolume()
    {
        if (BackgroundVolumeSlider.value == BackgroundVolumeSlider.minValue)
            AudioMixerMaster.SetFloat("ExposedBackgroundVolume", -80);
        else
            AudioMixerMaster.SetFloat("ExposedBackgroundVolume", BackgroundVolumeSlider.value);
    }

    /// <summary>
    /// Change the volume of the game
    /// </summary>
    public void SetSoundEffectsVolume()
    {
        if (SoundEffectsVolumeSlider.value == SoundEffectsVolumeSlider.minValue)
            AudioMixerMaster.SetFloat("ExposedSoundEffectsVolume", -80);
        else
            AudioMixerMaster.SetFloat("ExposedSoundEffectsVolume", SoundEffectsVolumeSlider.value);
    }
}
