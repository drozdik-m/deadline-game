using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Class controlls all tabs settings and there UI representation. 
/// </summary>
public class MenuTabsManager : MonoBehaviour
{
    /// <summary>
    /// Active tab
    /// </summary>
    public Tab ActiveTab;

    /// <summary>
    /// Gameobject that has all tabs children
    /// </summary>
    public GameObject Tabs;

    /// <summary>
    /// All children tabs of GameObject Tabs
    /// </summary>
    private readonly List<Tab> allTabs = new List<Tab> ();

    void Start()
    {
        // Collect all tabs from Tabs object 
        foreach (var tab in Tabs.gameObject.GetComponentsInChildren<Tab>())
        {
            allTabs.Add(tab);
            CloseTab(tab);
        }

        // At the begining first tab will be active as default 
        ActiveTab = allTabs[0];
        CloseMenu();
    }

    /// <summary>
    /// Opens menu
    /// </summary>
    public void OpenMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        OpenTab(ActiveTab);
    }

    /// <summary>
    /// Closes menu
    /// </summary>
    public void CloseMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Activates tab
    /// </summary>
    /// <param name="tab">Tab, that should be opened</param>
    public void OpenTab(Tab tab)
    {
        ActiveTab = tab;
        tab.gameObject.SetActive(true);
    }

    /// <summary>
    /// Closes tab
    /// </summary>
    /// <param name="tab">Tab, that should be closed</param>
    public void CloseTab(Tab tab)
    {
        tab.gameObject.SetActive(false);
    }

    /// <summary>
    /// Saves all settings
    /// </summary>
    public void SaveSettingsData()
    {
        if(EditorUtility.DisplayDialog("Save settings", "Do you want to save changes?", "Yes", "No"))
        {
            foreach (var item in allTabs)
            {
                item.SaveData();
            }
            PlayerPrefs.SetInt("Saved", 1);
        }
    }
}
