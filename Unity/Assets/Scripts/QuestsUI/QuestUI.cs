using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class represents the quest image and description on the UI
/// </summary>
public class QuestUI : MonoBehaviour
{
    /// <summary>
    /// Description of the quest
    /// </summary>
    public Text QuestDescriptionText;

    /// <summary>
    /// Image represents status of the quest
    /// </summary>
    public Image QuestStatusImage;

    /// <summary>
    /// Sets new quest status on the UI
    /// </summary>
    /// <param name="questDescription">Description text of the quest</param>
    /// <param name="questStatusColor">Color of the status image</param>
    /// <param name="textAlphaColor">Alpha of the text description</param>
    public void SetNewUI(string questDescription, Color questStatusColor, float textAlphaColor)
    {
        Color color = QuestDescriptionText.color;
        color.a = textAlphaColor;
        questStatusColor.a = textAlphaColor;

        QuestDescriptionText.color = color;
        QuestDescriptionText.text = questDescription;
        QuestStatusImage.color = questStatusColor;
    }
}
