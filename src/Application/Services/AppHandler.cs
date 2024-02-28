using Application.Abstracts;
using Application.Interactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class AppHandler : IAppHandler
{
	public IInteractor Interactor { get; } 

	private readonly IAppStateChecker _stateChecker;
	private readonly IAppTimeTracker _timeTracker;
	private readonly IAppIOService _ioService;
	
	public AppHandler(IInteractor interactor)
	{
		Interactor = interactor;


		_stateChecker = new AppStateChecker(Interactor);
		_timeTracker = new AppTimeTracker();
		_ioService = new AppIOService();


		var timerSingleton = StaticTimerService.GetInstance();
		timerSingleton.TimeElapsed -= OnTimerElapsed;
		timerSingleton.TimeElapsed += OnTimerElapsed;
	}

	public void StartTrackingApp()
	{
		_stateChecker.GetAppState();
		
	}

	private void OnTimerElapsed(object? sender, int e)
	{
		StartTrackingApp();
	}
}
