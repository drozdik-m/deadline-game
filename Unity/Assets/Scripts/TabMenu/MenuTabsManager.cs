using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuTabsManager : MonoBehaviour
{
    public Tab ActiveTab;
    public GameObject Tabs;

    private List<Tab> _allTabs = new List<Tab> ();

    // Start is called before the first frame update
    void Start()
    {
        // Collect all tabs from Tabs object 
        foreach (var tab in Tabs.gameObject.GetComponentsInChildren<Tab>())
        {
            _allTabs.Add(tab);
            CloseTab(tab);
        }
        // At th begining first tab will be active as default 
        ActiveTab = _allTabs[0];
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

    public void SaveSettingsData()
    {
        if(EditorUtility.DisplayDialog("Save settings", "Do you want to save changes in settings?", "Yes", "No"))
            foreach (var item in _allTabs)
            {
                item.SaveData();
            }
    }
}
