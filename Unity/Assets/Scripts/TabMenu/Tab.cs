using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour
{
    private MenuTabsManager _tabsManager;

    protected void Awake()
    {
        _tabsManager = GameObject.FindGameObjectWithTag("MenuTabsManager").GetComponent<MenuTabsManager>();
    }

    protected void Start()
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

    public virtual void LoadData()
    {
        Debug.Log("Restore settings in " + transform.name);
    }

    public virtual void SaveData()
    {
        Debug.Log("Save settings in " + transform.name);
        PlayerPrefs.SetInt("Saved", 1);
    }
}
