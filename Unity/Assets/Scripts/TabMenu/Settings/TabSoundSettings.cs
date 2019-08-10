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

        AudioMixerMaster.SetFloat("ExposedMasterVolume", MasterVolumeSlider.value);
        AudioMixerMaster.SetFloat("ExposedBackgroundVolume", BackgroundVolumeSlider.value);
        AudioMixerMaster.SetFloat("ExposedSoundEffectsVolume", SoundEffectsVolumeSlider.value);
    }

    public override void LoadData()
    {
        base.LoadData();
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        BackgroundVolumeSlider.value = PlayerPrefs.GetFloat("BackgroundVolume");
        SoundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume");
    }
}
