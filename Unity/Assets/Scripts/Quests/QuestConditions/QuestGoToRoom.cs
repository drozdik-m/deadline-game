using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Condition that checks current room
/// </summary>
public class QuestGoToRoom : QuestManualCondition
{
    /// <summary>
    /// Target room
    /// </summary>
    public RoomList targetRoom;

    /// <summary>
    /// Used room manager
    /// </summary>
    RoomManager roomManager;

    private void Start()
    {
        roomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
        roomManager.RoomChanged += OnRoomChange;
        CheckCondition();
    }

    private void OnRoomChange(object source, RoomList room)
    {
        CheckCondition();
    }

    /// <summary>
    /// Checks if the player is in the target room
    /// </summary>
    private void CheckCondition()
    {
        if (roomManager.CurrentRoom == targetRoom)
            Completed = true;
        else
            Completed = false;
    }
}
