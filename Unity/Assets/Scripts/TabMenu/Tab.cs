using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour
{
    private MenuTabsManager _tabsManager;

    // Start is called before the first frame update
    void Awake()
    {
        _tabsManager = GameObject.FindGameObjectWithTag("MenuTabsManager").GetComponent<MenuTabsManager>();
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
}
