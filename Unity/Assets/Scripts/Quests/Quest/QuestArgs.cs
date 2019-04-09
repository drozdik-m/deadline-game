
/// <summary>
/// Quest event arguments
/// </summary>
public class QuestArgs
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

    public QuestArgs(bool completed)
    {
        this.completed = completed;
    }
}