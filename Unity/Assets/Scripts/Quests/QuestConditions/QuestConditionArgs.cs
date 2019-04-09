

/// <summary>
/// Quest condition arguments class
/// </summary>
public class QuestConditionArgs
{
    /// <summary>
    /// Is the condition completed now?
    /// </summary>
    public bool Completed
    {
        get
        {
            return completed;
        }
    }

    private readonly bool completed = false;

    public QuestConditionArgs(bool completed)
    {
        this.completed = completed;
    }
}