using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public Text QuestDescriptionText;
    public Image QuestStatusImage;

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
