using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Condition collection for specific interactable in the scene.
/// It represent the desired state of conditions and is used to
/// be compared with conditions in AllConditions.
/// </summary>
public class ConditionCollection : ScriptableObject
{
    /// <summary>
    /// Describes what condition collection is suppose to check
    /// </summary>
    public string description;

    /// <summary>
    /// Conditions that represent the required conditions
    /// </summary>
    public Condition[] requiredConditions = new Condition[0];

    /// <summary>
    /// Reactions that will be played if all conditions are met
    /// </summary>
    public ReactionCollection reactionCollection;

    /// <summary>
    /// Checks if all required conditions are met and if they are play all reactions
    /// </summary>
    /// <returns>True if all required conditions were met, false if not</returns>
    public bool CheckAndReact()
    {
        for (int i = 0; i < requiredConditions.Length; i++)
            if (!AllConditions.CheckCondition(requiredConditions[i]))
                return false; // condition is not met

        // all required conditions were met, play the reactions
        if (reactionCollection)
            reactionCollection.React();

        return true;
    }
}
