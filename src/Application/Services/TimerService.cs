using Application.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Application.Services;


public class TimerService : ITimerService
{
	private readonly System.Timers.Timer _timer;

	public event EventHandler<int>? TimeElapsed;

	public TimerService()
	{
		_timer = new System.Timers.Timer(1000);
		_timer.Elapsed += OnTimerElapsed;
		_timer.AutoReset = true;
		_timer.Enabled = true;
	}

	private void OnTimerElapsed(object? sender, EventArgs e)
	{
		TimeElapsed?.Invoke(this, 1000);
	}
}
