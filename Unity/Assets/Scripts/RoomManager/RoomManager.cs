using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    /// <summary>
    /// Room changed event handler.
    /// </summary>
    public delegate void RoomChangedEventHandler(object source, RoomList room);
    /// <summary>
    /// The current room.
    /// </summary>
    private RoomList _currentRoom;
    /// <summary>
    /// Gets or sets the current room.
    /// </summary>
    /// <value>The current room.</value>
    public RoomList CurrentRoom
    {
        get
        {
            return this._currentRoom;
        }

        set
        {
            _currentRoom = value;
            OnRoomChanged();
        }
    }
    /// <summary>
    /// Occurs when room changed.
    /// </summary>
    public event RoomChangedEventHandler RoomChanged;
    /// <summary>
    /// On the room changed trigger.
    /// </summary>
    protected virtual void OnRoomChanged()
    {
        if (RoomChanged != null)
            RoomChanged(this, CurrentRoom);
    }

}
