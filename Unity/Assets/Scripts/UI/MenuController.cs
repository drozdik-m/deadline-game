using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class control button events in Menu
/// </summary>
public class MenuController : MonoBehaviour
{
    /// <summary>
    /// Options Menu
    /// </summary>
    public GameObject OptionsPanel;

    private void Start()
    {
        OptionsPanel.SetActive (false);
    }

    /// <summary>
    /// Open Options Menu
    /// </summary>
    public void OpenOptionsMenu()
    {
        OptionsPanel.SetActive (true);
    }

    /// <summary>
    /// Close Options Menu
    /// </summary>
    public void CloseOptionsMenu()
    {
        OptionsPanel.SetActive (false);
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitTheGame()
    {
        Application.Quit ();
    }
}
