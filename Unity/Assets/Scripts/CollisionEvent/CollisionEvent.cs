using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wrapper for trigger and collision events on foreign objects
/// </summary>
public class CollisionEvent : MonoBehaviour
{
    /// <summary>
    /// Called on trigger enter
    /// </summary>
    public event OnTriggerHandler OnTriggerEnterEvent;

    /// <summary>
    /// Called on trigger leave
    /// </summary>
    public event OnTriggerHandler OnTriggerLeaveEvent;


    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerLeaveEvent?.Invoke(this, other);
    }
}

public delegate void OnColisionHandler(CollisionEvent caller, Collider otherCollider);
public delegate void OnTriggerHandler(CollisionEvent caller, Collider otherCollider);
