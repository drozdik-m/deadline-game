using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour
{
    private MenuTabsManager _tabsManager;

    // Start is called before the first frame update
    void Start()
    {
        _tabsManager = GameObject.FindGameObjectWithTag("MenuTabManager").GetComponent<MenuTabsManager>();
    }

    public void Open()
    {

    }

    public bool isActive()
    {
        return gameObject.activeSelf;
    }
}
