using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Condition that checks current room
/// </summary>
public class QuestGoToRoom : QuestCondition
{
    /// <summary>
    /// Target room
    /// </summary>
    public RoomList TargetRoom;

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

    private void OnRoomChange(RoomManager source, RoomManagerArgs args)
    {
        CheckCondition();
    }

    /// <summary>
    /// Checks if the player is in the target room
    /// </summary>
    private void CheckCondition()
    {
        if (roomManager.CurrentRoom == TargetRoom)
            Completed = true;
        else
            Completed = false;
    }
}
