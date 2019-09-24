using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// User Interface manager
/// </summary>
public class MenuUIManager : MonoBehaviour
{
    /// <summary>
    /// Menu panel
    /// </summary>
    public GameObject MenuPanel;

    /// <summary>
    /// Status of menu (opened or closed)
    /// </summary>
    private bool menuOpen;

    /// <summary>
    /// Option menu with tabs
    /// </summary>
    private MenuTabsManager optionTabsMenu;

    private void Start()
    {
        MenuPanel.SetActive (false);
        menuOpen = false;
        optionTabsMenu = GameObject.FindGameObjectWithTag("MenuTabsManager").GetComponent<MenuTabsManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            ChangeMenuStatus ();
        }
    }

    /// <summary>
    /// Open or close Menu. If Menu is open, stop background scene.
    /// </summary>
    public void ChangeMenuStatus()
    {
        // Change value to opposite
        menuOpen = !menuOpen;

        if (menuOpen)
            Time.timeScale = 0f;
        else
        {
            Time.timeScale = 1f;

            // Close Option panel, if it was opened
            optionTabsMenu.CloseMenu ();
        }
        MenuPanel.SetActive (menuOpen);
    }
}
