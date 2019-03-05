using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractButton : MonoBehaviour
{
    public GameObject InterectObject;
    public Button button;

    private void Start()
    {
        button.gameObject.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        if ( Vector3.Distance( InterectObject.transform.position, transform.position ) < 100)
        {
            Debug.Log ("Object is close enough");
            //Transform canvas = transform.GetComponentInChildren<Transform> ();
            button.gameObject.SetActive (true);
        }
    }
}
