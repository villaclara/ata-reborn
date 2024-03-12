using Application.AppToTrack.Abstracts;
using Application.AppToTrack.Enums;
using Application.AppToTrack.Interactors;
using Application.Common.Timers;
using Serilog;
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
        AppInstanceState isRunning = IsRunningByName(AppInteractor.GetAppInstace().ProcessNameInOS)
			? AppInstanceState.Running
			: AppInstanceState.Stopped;
        
        Log.Information("{@App} - {@Method} returned {@isRunning}", AppInteractor.GetAppInstace().ProcessNameInOS, nameof(GetAppState), isRunning);
        return isRunning;
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
        app.IsRunning = appInstanceState == AppInstanceState.Running;
        if(app.IsRunning)
        {
            app.LastRunningDate = DateTime.Now;
        }
        app.LastCheckedDate = DateTime.Now;

        Log.Information("{@App} - {@Method} - {@LastRunningDate} - {@Value}", app.ProcessNameInOS, nameof(SetAppState), nameof(app.LastRunningDate), app.LastRunningDate);
        Log.Information("{@App} - {@Method} - {@LastRunningDate} - {@Value}", app.ProcessNameInOS, nameof(SetAppState), nameof(app.LastCheckedDate), app.LastCheckedDate);
    }
}
