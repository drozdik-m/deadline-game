using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTabsManager : MonoBehaviour
{
    public Tab ActiveTab;
    public GameObject Tabs;

    private List<Tab> AllTabs = new List<Tab> ();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var tab in Tabs.gameObject.GetComponentsInChildren<Tab>())
        {
            AllTabs.Add(tab);
            CloseTab(tab);
        }
        ActiveTab = AllTabs[0];
        CloseMenu();
    }

    public void OpenMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        OpenTab(ActiveTab);
    }

    public void CloseMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void OpenTab(Tab tab)
    {
        ActiveTab = tab;
        tab.gameObject.SetActive(true);
    }

    public void CloseTab(Tab tab)
    {
        tab.gameObject.SetActive(false);
    }
}
