using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// User Interface manager
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Menu panel
    /// </summary>
    public GameObject MenuPanel;
    private bool menuOpen;

    private void Start()
    {
        MenuPanel.SetActive (false);
        menuOpen = false;
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
            Transform optionPanel = MenuPanel.transform.Find ("OptionsPanel");
            optionPanel.gameObject.SetActive (false);
        }

        MenuPanel.SetActive (menuOpen);
    }
}
