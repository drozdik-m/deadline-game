using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Class control button events in Menu
/// </summary>
public class MenuController : MonoBehaviour
{
    /// <summary>
    /// Options Menu
    /// </summary>
    public GameObject OptionsPanel;

    /// <summary>
    /// Audio mixer 
    /// </summary>
    public AudioMixer AudioMixerMaster;

    /// <summary>
    /// Slider that keep master volume value
    /// </summary>
    public Slider MasterVolumeSlider;

    /// <summary>
    /// Slider that keep background volume value
    /// </summary>
    public Slider BackgroundVolumeSlider;

    /// <summary>
    /// Slider that keep sound effects volume value
    /// </summary>
    public Slider SoundEffectsVolumeSlider;

    private void Start()
    {
        OptionsPanel.SetActive (false);
    }

    /// <summary>
    /// Open Options Menu
    /// </summary>
    public void OpenOptionsMenu()
    {
        OptionsPanel.SetActive (true);
    }

    /// <summary>
    /// Close Options Menu
    /// </summary>
    public void CloseOptionsMenu()
    {
        OptionsPanel.SetActive (false);
    }
    /// <summary>
    /// Change the master volume of the game
    /// </summary>
    public void SetMasterVolume()
    {
        AudioMixerMaster.SetFloat ("ExposedMasterVolume", MasterVolumeSlider.value);
    }

    /// <summary>
    /// Change the background volume of the game
    /// </summary>
    public void SetBackgroundVolume()
    {
        AudioMixerMaster.SetFloat ("ExposedBackgroundVolume", BackgroundVolumeSlider.value);
    }

    /// <summary>
    /// Change the volume of the game
    /// </summary>
    public void SetSoundEffectsVolume()
    {
        AudioMixerMaster.SetFloat ("ExposedSoundEffectsVolume", SoundEffectsVolumeSlider.value);
    }

    /// <summary>
    /// Start the game
    /// </summary>
    public void StartGame()
    {
        FindObjectOfType<ScenesWorkflow>().NextScene();
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitTheGame()
    {
        Application.Quit ();
    }
}
