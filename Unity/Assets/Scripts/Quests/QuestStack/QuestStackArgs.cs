/// <summary>
/// Quest stack event arguments
/// </summary>
public class QuestStackArgs
{
    /// <summary>
    /// Are all quests completed?
    /// </summary>
    public bool Completed
    {
        get
        {
            return completed;
        }
    }

    private readonly bool completed = false;

    public QuestStackArgs(bool completed)
    {
        this.completed = completed;
    }
}