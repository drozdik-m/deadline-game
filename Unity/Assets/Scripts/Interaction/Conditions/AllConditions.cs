using UnityEngine;

/// <summary>
/// Global conditions in the game
/// </summary>
public class AllConditions : ResettableScriptableObject
{
    private const string loadPath = "AllConditions";

    /// <summary>
    /// Conditions that represent the state of the game
    /// </summary>
    public Condition[] conditions;

    /// <summary>
    /// instance of all conditions. This gives access to the AllCondition
    /// </summary>
    private static AllConditions instance;

    /// <summary>
    /// Get the instance of AllConditions
    /// </summary>
    public static AllConditions Instance
    {
        // we need to check if the instance exists
        get
        {
            if (!instance)
                instance = FindObjectOfType<AllConditions>();
            if (!instance)
                instance = Resources.Load<AllConditions>(loadPath);
            if (!instance)
                Debug.LogError("AllConditions has not been created yet. Go to Assets -> Create -> AllConditions.");
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    /// <summary>
    /// Resets all conditions. Used when the game is started again.
    /// </summary>
    public override void Reset()
    {
        if (conditions == null) return;

        for (int i = 0; i < conditions.Length; i++)
            conditions[i].satisfied = false;
    }

    /// <summary>
    /// Compare condition set in the scene and the one in the AllConditions
    /// </summary>
    /// <param name="requiredCondition">Condition to compare the global one to</param>
    /// <returns>True if the condition is satisfied, false if not</returns>
    public static bool CheckCondition(Condition requiredCondition)
    {
        Condition[] allConditions = Instance.conditions;

        // the global condition we want to compare with the parameter one
        Condition globalCondition = null;

        // check if the condition exists in allConditions
        // if yes, save it to globalCondition
        if (allConditions != null && allConditions[0] != null)
            for (int i = 0; i < allConditions.Length; i++)
                if (allConditions[i].hash == requiredCondition.hash)
                    globalCondition = allConditions[i];

        // we did not find the global condition
        if (!globalCondition) return false;

        return globalCondition.satisfied == requiredCondition.satisfied;
    }

}
