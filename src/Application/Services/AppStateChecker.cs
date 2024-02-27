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
		//_timerService = timerService;
		//_timerService.TimeElapsed += OnA;

		var tim = StaticTimerService.GetInstance();
		tim.TimeElapsed += OnA;

	}

	public IInteractor AppInteractor => throw new NotImplementedException();

	public void GetAppState()
	{

		
	}

	public void OnA(object? sender, int e)
	{
		GetAppState();
    }

	public void SetAppState(AppInstanceState appInstanceState = AppInstanceState.Stopped)
	{
		throw new NotImplementedException();
	}
}
