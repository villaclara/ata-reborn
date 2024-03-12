using Application.AppToTrack.Abstracts;
using Application.AppToTrack.Interactors;
using Application.Common.Timers;
using Serilog;

namespace Application.AppToTrack.Services;

public class AppHandler : IAppHandler
{
    public IInteractor Interactor { get; }

    private readonly IAppStateChecker _stateChecker;
    private readonly IAppTimeTracker _timeTracker;

    public AppHandler(IInteractor interactor)
    {
        Interactor = interactor;


        _stateChecker = new AppStateChecker(Interactor);
        _timeTracker = new AppTimeTracker(Interactor);


        //var timerSingleton = StaticTimerService.GetInstance();
        //timerSingleton.TimeElapsed -= OnTimerElapsed;
        //timerSingleton.TimeElapsed += OnTimerElapsed;
    }

    public  void StartTrackingApp()
    {
        Log.Information("Start method {@Method} for app {@App}", nameof(StartTrackingApp), Interactor.GetAppInstace().ProcessNameInOS);

        var state = _stateChecker.GetAppState();
        _stateChecker.SetAppState(state);
        _timeTracker.TrackTime();

        Log.Information("End method {@Method} for app {@App}", nameof(StartTrackingApp), Interactor.GetAppInstace().ProcessNameInOS);
    }

    private void OnTimerElapsed(object? sender, int e)
    {
       // StartTrackingApp();
    }
}
