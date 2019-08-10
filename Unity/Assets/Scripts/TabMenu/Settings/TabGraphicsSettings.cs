using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGraphicsSettings : Tab
{
    public Dropdown ResolutionDropdown;
    public Dropdown QualityDropdown;
    public Toggle FullscreenToogle;

    private Resolution[] _resolutions;

    new void Awake()
    {
        base.Awake();

        List<string> options = new List<string>();

        _resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();

        foreach (var item in _resolutions)
        {
            string option = item.width + " x " + item.height;
            options.Add(option);
        }

        ResolutionDropdown.AddOptions(options);
    }


    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(QualityDropdown.value);
    }

    public void SetResolution()
    {
        Resolution resolution = _resolutions[ResolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = FullscreenToogle.isOn;
    }

    public override void SaveData()
    {
        base.SaveData();
        PlayerPrefs.SetInt("Resolution index", ResolutionDropdown.value);
        PlayerPrefs.SetInt("Fullscreen", Convert.ToInt32(FullscreenToogle.isOn));
        PlayerPrefs.SetInt("Quality index", QualityDropdown.value);
    }

    public override void LoadData()
    {
        base.LoadData();
        FullscreenToogle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen"));
        QualityDropdown.value = PlayerPrefs.GetInt("Quality index");
        ResolutionDropdown.value = PlayerPrefs.GetInt("Resolution index");

        SetFullscreen();
        SetQuality();
        SetResolution();
    }
}
