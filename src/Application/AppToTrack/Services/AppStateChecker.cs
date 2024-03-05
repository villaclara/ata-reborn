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

    public AppInstanceState GetAppState()
    {
        var procs = Process.GetProcesses();
        foreach (var pro in procs)
        {
            if (pro.ProcessName == AppInteractor.GetAppInstace().ProcessNameInOS)
            {
                Console.WriteLine($"{pro.ProcessName} running");
                return AppInstanceState.Running;
            }
        }

        return AppInstanceState.Stopped;

    }


    public void SetAppState(AppInstanceState appInstanceState = AppInstanceState.Stopped)
    {
        var app = AppInteractor.GetAppInstace();
        app.IsRunning = appInstanceState == AppInstanceState.Running ? true : false;
        Console.WriteLine($"App {app.Name} set to state - {appInstanceState}");
        app.CurrentSessionTime += 5;
    }
}
