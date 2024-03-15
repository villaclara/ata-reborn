using Application.Common.Abstracts;
using Application.Common.Timers;
using Application.Models;
using Application.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Application.Common.Services;

public class MainWorker 
{

	public event Func<object, int, Task>? WorkDone;

	private StaticTimerService _timer = StaticTimerService.GetInstance();

	public void Run()
	{
		_timer.TimeElapsed += DoWork;
	}

	public async Task DoWork(object? sender, int value)
	{
		// do some work here
		Log.Information("Method DOWORK called.");

		await Task.Delay(1000);
		OnWorkDone();
	}

	private void OnWorkDone()
	{
		WorkDone?.Invoke(this, 1);
	}


}
