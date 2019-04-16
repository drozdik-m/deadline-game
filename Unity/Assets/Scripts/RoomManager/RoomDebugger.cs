using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDebugger : MonoBehaviour
{
    public DoorKeeper door;
    public RoomManager rm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
            door.Teleport();
        Debug.Log(rm.CurrentRoom);
           
    }
}
