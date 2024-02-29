using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Application.Timers;

/// <summary>
/// Class responsible for setting timer and fire event when timer has elapsed.
/// </summary>
public class TimerService : ITimerService
{
    // Public event that subscribers want to subscribe
    public event EventHandler<int>? TimeElapsed;

    private readonly System.Timers.Timer _timer;

    public const int TIME_INTERVAL = 1000;

    public TimerService()
    {
        // Change value whenever we want to be different than 1sec
        _timer = new System.Timers.Timer(TIME_INTERVAL);
        _timer.Elapsed += OnTimerElapsed;
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }

    private void OnTimerElapsed(object? sender, EventArgs e)
    {
        // Change value here too
        TimeElapsed?.Invoke(this, TIME_INTERVAL);
    }
}

