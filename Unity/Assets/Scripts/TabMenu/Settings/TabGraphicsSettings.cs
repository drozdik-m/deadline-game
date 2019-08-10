using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class represent graphics settings
/// </summary>
public class TabGraphicsSettings : Tab
{
    /// <summary>
    /// Dropdown for resolution of the screen
    /// </summary>
    public Dropdown ResolutionDropdown;

    /// <summary>
    /// Dropdonw for quality graphics
    /// </summary>
    public Dropdown QualityDropdown;

    /// <summary>
    /// Toggle controls fullscreen mode
    /// </summary>
    public Toggle FullscreenToogle;

    /// <summary>
    /// Store all resulitions 
    /// </summary>
    private Resolution[] _resolutions;

    new void Awake()
    {
        base.Awake();

        List<string> options = new List<string>();

        // Add all existing resolutions
        _resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();

        foreach (var item in _resolutions)
        {
            string option = item.width + " x " + item.height;
            options.Add(option);
        }

        ResolutionDropdown.AddOptions(options);
    }

    /// <summary>
    /// Sets graphics quality from Quality dropdown
    /// </summary>
    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(QualityDropdown.value);
    }

    /// <summary>
    /// Sets resolution of the screen from Resolution dropdown
    /// </summary>
    public void SetResolution()
    {
        Resolution resolution = _resolutions[ResolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    /// <summary>
    /// Sets fullscreen mode from Fullscreen toogle
    /// </summary>
    public void SetFullscreen()
    {
        Screen.fullScreen = FullscreenToogle.isOn;
    }

    /// <summary>
    /// Saves sound settings to PlayerPrefs
    /// </summary>
    public override void SaveData()
    {
        PlayerPrefs.SetInt("Resolution index", ResolutionDropdown.value);
        PlayerPrefs.SetInt("Fullscreen", Convert.ToInt32(FullscreenToogle.isOn));
        PlayerPrefs.SetInt("Quality index", QualityDropdown.value);
    }

    /// <summary>
    /// Loads sound settings to PlayerPrefs
    /// </summary>
    public override void LoadData()
    {
        FullscreenToogle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen"));
        QualityDropdown.value = PlayerPrefs.GetInt("Quality index");
        ResolutionDropdown.value = PlayerPrefs.GetInt("Resolution index");

        SetFullscreen();
        SetQuality();
        SetResolution();
    }
}
