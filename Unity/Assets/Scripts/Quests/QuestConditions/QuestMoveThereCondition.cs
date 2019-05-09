using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Condition for someone to move somewhere
/// </summary>
public class QuestMoveThereCondition : QuestCondition
{
    /// <summary>
    /// Override collision trigger (this object by default)
    /// </summary>
    public CollisionEvent OverrideCollisionTrigger;

    /// <summary>
    /// Tag of the object who should move to desired location
    /// </summary>
    public string CollisionConditionTag = "Player";

    /// <summary>
    /// Actual collision event
    /// </summary>
    CollisionEvent collisionEvent;


    private void Start()
    {
        collisionEvent = OverrideCollisionTrigger != null ? OverrideCollisionTrigger :
            GetComponent<CollisionEvent>();

        collisionEvent.OnTriggerEnterEvent += OnSomeObjectTriggerEnter;
        collisionEvent.OnTriggerLeaveEvent += OnSomeObjectTriggerLeave;
    }

    private void OnSomeObjectTriggerLeave(CollisionEvent caller, Collider otherCollider)
    {
        if (otherCollider.tag == CollisionConditionTag)
            Completed = false;
    }

    private void OnSomeObjectTriggerEnter(CollisionEvent caller, Collider otherCollider)
    {
        if (otherCollider.tag == CollisionConditionTag)
            Completed = true;
    }
}
