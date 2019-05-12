using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Condition for someone to move somewhere
/// </summary>
public class QuestMoveThereCondition : QuestCondition
{
    /// <summary>
    /// Vollision trigger (this object by default)
    /// </summary>
    public CollisionEvent CollisionTrigger = null;

    /// <summary>
    /// Tag of the object who should move to desired location
    /// </summary>
    public string CollisionConditionTag = "Player";


    private void Start()
    {
        if (CollisionTrigger == null)
            CollisionTrigger = GetComponent<CollisionEvent>();

        CollisionTrigger.OnTriggerEnterEvent += OnSomeObjectTriggerEnter;
        CollisionTrigger.OnTriggerLeaveEvent += OnSomeObjectTriggerLeave;
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
