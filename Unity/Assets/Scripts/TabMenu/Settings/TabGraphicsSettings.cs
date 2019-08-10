using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGraphicsSettings : Tab
{
    public Dropdown ResolutionDropdown;
    public Toggle FullscreenToogle;

    private Resolution[] _resolutions;

    new void Start()
    {
        base.Start();

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

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = FullscreenToogle.isOn;
    }

    public override void SaveData()
    {
        base.SaveData();
        PlayerPrefs.SetInt("Resolution width", Screen.currentResolution.width);
        PlayerPrefs.SetInt("Resolution height", Screen.currentResolution.height);
        PlayerPrefs.SetInt("Fullscreen", Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetInt("Quality", QualitySettings.GetQualityLevel());
    }

    public override void LoadData()
    {
        base.LoadData();

        FullscreenToogle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen"));
        int qualityIndex = PlayerPrefs.GetInt("Quality");
        int screenWidth = PlayerPrefs.GetInt("Resolution width");
        int screenHeight = PlayerPrefs.GetInt("Resolution height");

        SetFullscreen();
        SetQuality(qualityIndex);
        Screen.SetResolution(screenWidth, screenHeight, Screen.fullScreen);
    }
}
