using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{

    public Transform player;
    private Transform label;
    public Vector3 offset = new Vector3(1.33f, 1.3f, -1.47f);
    // Start is called before the first frame update
    void Start()
    {
        label = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        label.position = player.position + offset;
    }
}
