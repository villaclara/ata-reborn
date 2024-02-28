using Application.Abstracts;
using Application.Enums;
using Application.Interactors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class AppStateChecker : IAppStateChecker
{
	private readonly ITimerService _timerService;

	public AppStateChecker(ITimerService timerService)
	{
		//_timerService = timerService;
		//_timerService.TimeElapsed += OnA;


	}


	
	public AppStateChecker(IInteractor interactor)
	{
		AppInteractor = interactor;
	}

	public IInteractor AppInteractor { get; }

	public void GetAppState()
	{
		var procs = Process.GetProcesses();
		foreach (var pro in procs)
		{
			if (pro.ProcessName == "devenv")
			{
				Console.WriteLine($"{pro.ProcessName} running");
			}
		}

	}


	public void SetAppState(AppInstanceState appInstanceState = AppInstanceState.Stopped)
	{
		throw new NotImplementedException();
	}
}
