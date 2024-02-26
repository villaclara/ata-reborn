using Application.Abstracts;
using Application.Enums;
using Application.Interactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class AppStateChecker : IAppStateChecker
{
	private readonly ITimerService _timerService;

	public AppStateChecker(ITimerService timerService)
	{
		_timerService = timerService;
		_timerService.TimeElapsed += OnA;

	}

	public IInteractor AppInteractor => throw new NotImplementedException();

	public void GetAppState()
	{
		throw new NotImplementedException();
	}

	public void OnA(object? sender, int e)
	{
        Console.WriteLine("bruh");
    }

	public void SetAppState(AppInstanceState appInstanceState = AppInstanceState.Stopped)
	{
		throw new NotImplementedException();
	}
}
