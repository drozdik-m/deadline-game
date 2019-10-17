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

    /// <summary>
    /// Called on trigger stay
    /// </summary>
    public event OnTriggerHandler OnTriggerStayEvent;

    /// <summary>
    /// Called on collider enter
    /// </summary>
    public event OnColisionHandler OnCollisoinEnterEvent;

    /// <summary>
    /// Called on collider leave
    /// </summary>
    public event OnColisionHandler OnCollisoinLeaveEvent;

    /// <summary>
    /// Called on collider stay
    /// </summary>
    public event OnColisionHandler OnCollisoinStayEvent;


    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerLeaveEvent?.Invoke(this, other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerStayEvent?.Invoke(this, other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisoinEnterEvent?.Invoke(this, collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        OnCollisoinLeaveEvent?.Invoke(this, collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        OnCollisoinStayEvent?.Invoke(this, collision);
    }
}

public delegate void OnColisionHandler(CollisionEvent caller, Collision otherCollider);
public delegate void OnTriggerHandler(CollisionEvent caller, Collider otherCollider);
