using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitAndGiveUI : BuildableObjectUI
{
    public Slider progressSlider;

    public Text procentText;

    /// <summary>
    /// Wait and give item stage component
    /// </summary>
    public WaitAndGive waitAndGiveComponent;

    private float delay;

    public void SetUI(GameObject buildableObject, Text state, Slider progressProcentSlider)
    {
        base.SetUI(buildableObject, state);
        progressSlider = progressProcentSlider;
        procentText = progressSlider.GetComponentInChildren<Text>();
        waitAndGiveComponent = buildableGameObject.GetComponentInChildren<WaitAndGive>();
        delay = waitAndGiveComponent.Delay;

        // Consume items event
        waitAndGiveComponent.OnTransformationFinished += OnTransformationFinished;
    }

    public override void Activate()
    {
        progressSlider.gameObject.SetActive(true);
        UpdateStateText("Preparing");
        StartCoroutine(LoadProgressEnumerator());
    }

    public override void Deactivate()
    {
        progressSlider.gameObject.SetActive(false);
    }

    private IEnumerator LoadProgressEnumerator()
    {
        // Time, when the script was run
        float startTime = Time.time;
        // Fading step (depends on fading duration)
        float step = 0;
        // Completed procent of item
        int procent = 0;

        // Chages alpha channel every frame by step
        while (startTime + delay > Time.time)
        {
            step += (1 / delay) * Time.deltaTime;
            progressSlider.value = Mathf.Lerp(0, 100, step);

            procent = (int)progressSlider.value;
            procentText.text = procent.ToString() + "%";

            yield return null;
        }
    }

    /// <summary>
    /// Transformation of the item is finished
    /// </summary>
    /// <param name="source">Source.</param>
    /// <param name="consumeItemsStageArgs">Wait and give stage arguments.</param>
    public void OnTransformationFinished(BuildStage source, WaitAndGiveArgs consumeItemsStageArgs)
    {
        // When transformation finished
        UpdateStateText("Completed");
        StopAllCoroutines();
        progressSlider.value = 100.0f;
        procentText.text = "100%";
        progressSlider.gameObject.SetActive(false);
    }
}
