using Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Timers;

/// <summary>
/// Class used to retrieve Singleton instance of timer.
/// </summary>
public class StaticTimerService
{
    // event for subscribers to sub
    //public event EventHandler<int>? TimeElapsed;
    public event Func<object, int, Task>? TimeElapsed;

    private readonly System.Timers.Timer _timer;

    // private instance of the class
    private static StaticTimerService? _instance;

    // object for locking for thread safety
    private static readonly object objectLock = new();


    private StaticTimerService()
    {
        // assiginng values to timer
        _timer = new System.Timers.Timer(ConstantValues.TIMER_INTERVAL_MS);
        _timer.Elapsed += OnTimerElapsed;
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }

    /// <summary>
    /// Get singleton instance of <see cref="StaticTimerService"/> class.
    /// </summary>
    /// <returns></returns>
    public static StaticTimerService GetInstance()
    {
        if (_instance == null)
        {
            // thread safety
            lock (objectLock)
            {
                _instance = new StaticTimerService();
            }
        }

        return _instance;
    }

    /// <summary>
    /// Triggered when timer with TIME_INTERVAL miliseconds has elapsed.
    /// </summary>
    /// <param name="sender">this object.</param>
    /// <param name="e">Miliseconds elapsed.</param>
    private void OnTimerElapsed(object? sender, EventArgs e)
    {
        TimeElapsed?.Invoke(this, ConstantValues.TIMER_INTERVAL_MS);
    }
}
