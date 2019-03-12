using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controll day and night skybox and light of sun
/// </summary>
public class SkyboxController : MonoBehaviour
{
    public enum Time { day, night };

    /// <summary>
    /// Main Camera
    /// </summary>
    public Camera MainCamera;

    public Color day;

    public Time time;

    // Start is called before the first frame update
    void Start()
    {
        Quaternion rot = new Quaternion ();
        rot.eulerAngles = new Vector3 (130, 0f, 0f);
        transform.rotation = rot;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            ChangeTime ();
        }
    }

    void ChangeTime()
    {
        Quaternion rot = new Quaternion ();

        if (time == Time.day)
        {
            rot.eulerAngles = new Vector3 (260, 0, 0);
            GetComponent<Light> ().color = Color.black;
            MainCamera.backgroundColor = new Color (0.1067106f, 0.1125143f, 0.2075472f);
            time = Time.night;
        }
        else
        {
            rot.eulerAngles = new Vector3 (130, 0, 0);
            GetComponent<Light> ().color = Color.white;
            MainCamera.backgroundColor = new Color (0.75f, 1f, 1f);
            time = Time.day;
        }

        transform.rotation = rot;
    }
}
