using Application.AppToTrack.Abstracts;
using Application.AppToTrack.Enums;
using Application.AppToTrack.Interactors;
using Application.Timers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppToTrack.Services;

public class AppStateChecker : IAppStateChecker
{

    public AppStateChecker(IInteractor interactor)
    {
        AppInteractor = interactor;
    }

    public IInteractor AppInteractor { get; }

    public AppInstanceState GetAppState()
    {
        return IsRunningByName(AppInteractor.GetAppInstace().ProcessNameInOS) 
            ? AppInstanceState.Running 
            : AppInstanceState.Stopped;
    }

    private bool IsRunningByName(string appName)
    {
		foreach (var pro in Process.GetProcesses())
		{
			if (pro.ProcessName == appName)
			{
				return true;
			}
		}

        return false;
	}


    public void SetAppState(AppInstanceState appInstanceState = AppInstanceState.Stopped)
    {
        var app = AppInteractor.GetAppInstace();
        app.IsRunning = appInstanceState == AppInstanceState.Running ? true : false;
        Console.WriteLine($"App {app.Name} set to state - {appInstanceState}");
        app.CurrentSessionTime += 5;
    }
}
