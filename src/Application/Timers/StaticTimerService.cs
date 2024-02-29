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
    public event EventHandler<int>? TimeElapsed;

    private readonly System.Timers.Timer _timer;

    // private instance of the class
    private static StaticTimerService? _instance;

    // object for locking for thread safety
    private static readonly object objectLock = new();

    public const int TIME_INTERVAL = 5000;

    private StaticTimerService()
    {
        // assiginng values to timer
        _timer = new System.Timers.Timer(TIME_INTERVAL);
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
    /// <param name="sender">this object</param>
    /// <param name="e">Miliseconds elapsed</param>
    private void OnTimerElapsed(object? sender, EventArgs e)
    {
        TimeElapsed?.Invoke(null, TIME_INTERVAL);
    }
}
