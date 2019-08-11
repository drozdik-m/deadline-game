
using System.Collections.Generic;

public class WaitAndGiveArgs
{

/// <summary>
/// Gets a value indicating whether this <see cref="T:WaitAndGiveArgs"/> counter finished.
/// </summary>
/// <value><c>true</c> if counter finished; otherwise, <c>false</c>.</value>
    public bool CounterFinished
    {
        get
        {
            return counterFinished;
        }
    }
    /// <summary>
    /// Gets the delay of the transforming process.
    /// </summary>
    /// <value>The delay.</value>
    public float Delay
    {
        get
        {
            return delay;
        }
    }

    private readonly bool counterFinished = false;
    private readonly float delay;


    public WaitAndGiveArgs(bool counterFinished, float delay)
    {
        this.counterFinished = counterFinished;
        this.delay = delay;
    }
}
