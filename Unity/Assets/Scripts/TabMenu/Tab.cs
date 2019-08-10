using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class represent single tab for MenuTabs manager. Inherited by another classes, that will represent settings.
/// </summary>
public abstract class Tab : MonoBehaviour
{
    /// <summary>
    /// MenuTabs manager
    /// </summary>
    private MenuTabsManager _tabsManager;

    protected void Awake()
    {
        _tabsManager = GameObject.FindGameObjectWithTag("MenuTabsManager").GetComponent<MenuTabsManager>();
    }

    protected void Start()
    {
        bool load = Convert.ToBoolean(PlayerPrefs.GetInt("Saved"));

        // Checks if old settings were saved
        if (load)
        {
            LoadData();
        }
    }

    /// <summary>
    /// Opens Tab UI
    /// </summary>
    public void Open()
    {
        _tabsManager.CloseTab(_tabsManager.ActiveTab);
        _tabsManager.OpenTab(this);
    }

    /// <summary>
    /// Checks if tab is active
    /// </summary>
    /// <returns></returns>
    public bool isActive()
    {
        return gameObject.activeSelf;
    }

    /// <summary>
    /// Absract class for loading data from PlayerPrefs
    /// </summary>
    public abstract void LoadData();

    /// <summary>
    /// Absract class for saving data to PlayerPrefs
    /// </summary>
    public abstract void SaveData();
}
