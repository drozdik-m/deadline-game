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
    /// Slider that keep volume value
    /// </summary>
    public Slider VolumeSlider;

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
    /// Change the volume of the game
    /// </summary>
    public void SetVolume()
    {
        AudioMixerMaster.SetFloat ("ExposedMasterVolume", VolumeSlider.value);
        AudioMixerMaster.SetFloat ("ExposedBackgroundVolume", VolumeSlider.value);
        AudioMixerMaster.SetFloat ("ExposedSoundEffectsVolume", VolumeSlider.value);
    }

    /// <summary>
    /// Start the game
    /// </summary>
    public void StartGame()
    {
        FindObjectOfType<ScenesWorkflow> ().NextScene ();
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitTheGame()
    {
        Application.Quit ();
    }
}
