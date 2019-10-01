using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPointManager : MonoBehaviour
{
    /// <summary>
    /// The focus points locations.
    /// </summary>
    public GameObject[] FocusPointsLocations;
    /// <summary>
    /// The focus pointer model.
    /// </summary>
    private GameObject FocusPointerModel;
    /// <summary>
    /// The game objects queue.
    /// </summary>
    private Queue<GameObject> focusPointLocationsQueue;
    // Start is called before the first frame update
    void Start()
    {
        var FocusPointerModel = GameObject.FindGameObjectWithTag("FocusPointer");
        if (!FocusPointerModel)
        {
            Debug.LogError("FocusPointerManager: No focus pointer model found!");
        }
        focusPointLocationsQueue = new Queue<GameObject>();

        foreach(var i in FocusPointsLocations)
        {
            focusPointLocationsQueue.Enqueue(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            NextFocusPoint();
        }

    }

    void NextFocusPoint()
    {
        if (focusPointLocationsQueue.Count == 0)
            return;
        FocusPointerModel.transform.position = focusPointLocationsQueue.Dequeue().transform.position;
    }


}
