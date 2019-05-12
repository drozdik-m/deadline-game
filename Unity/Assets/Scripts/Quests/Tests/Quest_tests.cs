using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_tests : MonoBehaviour
{
    public QuestCondition condition1;
    public QuestCondition condition2;
    public QuestCondition condition3;
    public Quest quest1;
    public Quest quest2;
    public QuestStack questStack1;

    public Text debugText;

    string prefixtext = "-";

    void Start()
    {
        condition1.OnChange += OnConditionChange;
        condition2.OnChange += OnConditionChange;
        condition3.OnChange += OnConditionChange;
        quest1.OnChange += OnQuestChange;
        quest2.OnChange += OnQuestChange;
        questStack1.OnChange += OnQuestStackChange;
    }

    private void OnQuestStackChange(QuestStack caller, QuestStackArgs args)
    {
        UpdateUI();
    }

    private void OnQuestChange(Quest caller, QuestArgs args)
    {
        UpdateUI();
    }

    private void OnConditionChange(QuestCondition caller, QuestConditionArgs args)
    {
        UpdateUI();
    }

    public void SwitchCondition1()
    {
        condition1.Completed = !condition1.Completed;
    }

    public void SwitchCondition2()
    {
        condition2.Completed = !condition2.Completed;
    }

    public void SwitchCondition3()
    {
        condition3.Completed = !condition3.Completed;
    }

    public void UpdateUI()
    {
        prefixtext += "-";

        string updateText = prefixtext + Environment.NewLine;
        updateText += "Condition 1: " + condition1.ConditionMet() + Environment.NewLine;
        updateText += "Condition 2: " + condition2.ConditionMet() + Environment.NewLine;
        updateText += "Condition 3: " + condition3.ConditionMet() + Environment.NewLine;
        updateText += "Quest 1: " + quest1.IsCompleted() + Environment.NewLine;
        updateText += "Quest 2: " + quest2.IsCompleted() + Environment.NewLine;
        updateText += "Quest Stack 1: " + questStack1.QuestsAreCompleted() + Environment.NewLine;

        debugText.text = updateText;
    }
}
