using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Settings : MonoBehaviour
{
    private MenuTabsManager _tabsManager;

    void Awake()
    {
        _tabsManager = GameObject.FindGameObjectWithTag("MenuTabsManager").GetComponent<MenuTabsManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bool load = Convert.ToBoolean(PlayerPrefs.GetInt("Saved"));

        if (load)
        {
            LoadData();
        }
    }

    public void Open()
    {
        _tabsManager.CloseTab(_tabsManager.ActiveTab);
        _tabsManager.OpenTab(this);
    }

    public bool isActive()
    {
        return gameObject.activeSelf;
    }

    public abstract void LoadData();
    public abstract void SaveData();
}
