using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controll day and night color of skybox and light of sun
/// </summary>
public class SkyboxController : MonoBehaviour
{
    public enum Time { day, night };

    /// <summary>
    /// Main Camera
    /// </summary>
    public Camera MainCamera;

    /// <summary>
    /// Color for Camera background at the daytime
    /// </summary>
    public Color DayColor;

    /// <summary>
    /// Color for Camera background at the nighttime
    /// </summary>
    public Color NightColor;

    /// <summary>
    /// Direction of the sun's light at the daytime
    /// </summary>
    public Vector3 DayLightDirectionSun;

    /// <summary>
    /// Direction of the sun's light at the nighttime
    /// </summary>
    public Vector3 NightLightDirectionSun;

    /// <summary>
    /// Times of the day (could be day or night)
    /// </summary>
    public Time TimesOfDay;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera.clearFlags = CameraClearFlags.SolidColor;
        ChangeTime();
    }

    /// <summary>
    /// Change time of the day, depends on the variable TimesOfDay
    /// </summary>
    public void ChangeTime(Time newTime)
    {
        var direction = new Quaternion();

        TimesOfDay = newTime;

        if (TimesOfDay == Time.night)
        {
            direction.eulerAngles = NightLightDirectionSun;
            GetComponent<Light> ().color = Color.black;
            MainCamera.backgroundColor = NightColor;
        }
        else
        {
            direction.eulerAngles = DayLightDirectionSun;
            GetComponent<Light> ().color = Color.gray;
            MainCamera.backgroundColor = DayColor;
        }

        transform.rotation = direction;
    }

    public void ChangeTime()
    {
        if (TimesOfDay == Time.day)
            ChangeTime(Time.night);
        else
            ChangeTime(Time.day);
    }
}
