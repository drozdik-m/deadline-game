using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class controlls all tabs settings and there UI representation. 
/// </summary>
public class MenuTabsManager : MonoBehaviour
{
    /// <summary>
    /// Active tab
    /// </summary>
    private Tab activeTab;

    /// <summary>
    /// Game object that has all tabs children
    /// </summary>
    public GameObject Tabs;

    /// <summary>
    /// Game object that has all buttons children
    /// </summary>
    public GameObject Buttons;

    /// <summary>
    /// Prefab for Display dialog
    /// </summary>
    public DisplayDialog DisplayDialogUI;

    /// <summary>
    /// Is game saving at the moment
    /// </summary>
    public bool IsSaving { get; private set; } = false;

    /// <summary>
    /// All children tabs of GameObject Tabs
    /// </summary>
    private readonly List<Tab> allTabs = new List<Tab> ();

    void Start()
    {
        bool load = Convert.ToBoolean(PlayerPrefs.GetInt("Saved"));

        // Collect all tabs from Tabs object 
        foreach (var tab in Tabs.gameObject.GetComponentsInChildren<Tab>())
        {
            allTabs.Add(tab);
            if (load)
                tab.LoadData();

            CloseTab(tab);
        }

        // At the begining first tab will be active as default 
        activeTab = allTabs[0];
        CloseMenu();
    }

    /// <summary>
    /// Opens menu
    /// </summary>
    public void OpenMenu()
    {
        Tabs.SetActive(true);
        Buttons.SetActive(true);
        OpenTab(activeTab);
    }

    /// <summary>
    /// Closes menu
    /// </summary>
    public void CloseMenu()
    {
        Tabs.SetActive(false);
        Buttons.SetActive(false);
    }

    /// <summary>
    /// Activates tab
    /// </summary>
    /// <param name="tab">Tab, that should be opened</param>
    public void OpenTab(Tab tab)
    {
        activeTab = tab;
        tab.gameObject.SetActive(true);
        tab.ActivationButton.GetComponent<Image>().color = Color.gray;
        tab.ActivationButton.GetComponentInChildren<Text>().color = Color.white;
    }

    /// <summary>
    /// Closes tab
    /// </summary>
    /// <param name="tab">Tab, that should be closed</param>
    public void CloseTab(Tab tab)
    {
        tab.gameObject.SetActive(false);
        tab.ActivationButton.GetComponent<Image>().color = Color.white;
        tab.ActivationButton.GetComponentInChildren<Text>().color = Color.black;
    }

    /// <summary>
    /// Closes active tab
    /// </summary>
    public void CloseActiveTab()
    {
        activeTab.gameObject.SetActive(false);
        activeTab.ActivationButton.GetComponent<Image>().color = Color.white;
        activeTab.ActivationButton.GetComponentInChildren<Text>().color = Color.black;
    }

    /// <summary>
    /// Saves all settings
    /// </summary>
    public void SaveSettingsData()
    {
        var dialog = Instantiate(DisplayDialogUI, transform.position, transform.rotation, transform);

        StartCoroutine(AnswerSelectionEnumerator(dialog));
    }

    /// <summary>
    /// Waits until player choose one of the options
    /// </summary>
    /// <param name="dialog">Display dialog</param>
    /// <returns></returns>
    private IEnumerator AnswerSelectionEnumerator(DisplayDialog dialog)
    {
        IsSaving = true;

        // Wait until player chooses one of the options
        while (!dialog.FalseState && !dialog.TrueState)
        {
            yield return null;
        }

        if (dialog.TrueState)
        {
            foreach (var item in allTabs)
            {
                item.SaveData();
            }
            PlayerPrefs.SetInt("Saved", 1);
        }

        Destroy(dialog.gameObject);
        IsSaving = false;

        yield break;
    }
}
