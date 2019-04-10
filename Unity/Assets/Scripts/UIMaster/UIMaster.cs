using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that contains all parts of UI
/// </summary>
public class UIMaster : MonoBehaviour
{
    /// <summary>
    /// Menu UI that control all buttons in menu
    /// </summary>
    public MenuUIManager MenuUI;

    /// <summary>
    /// Inventory UI that show active item in the inventory
    /// </summary>
    public InventoryUIController InventoryUI;

    /// <summary>
    /// Quest UI that shows all active quests
    /// </summary>
    public QuestsUIController QuestUI;

    private void Start()
    {
        if (MenuUI == null)
            MenuUI = (MenuUIManager)FindObjectOfType (typeof (MenuUIManager));

        if (InventoryUI == null)
            InventoryUI = (InventoryUIController)FindObjectOfType (typeof (InventoryUIController));

        if (QuestUI == null)
            QuestUI = (QuestsUIController)FindObjectOfType (typeof (QuestsUIController));
    }
}
