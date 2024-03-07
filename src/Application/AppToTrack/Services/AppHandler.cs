using Application.AppToTrack.Abstracts;
using Application.AppToTrack.Interactors;
using Application.Timers;

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
        var state = _stateChecker.GetAppState();
        _stateChecker.SetAppState(state);
        _timeTracker.TrackTime();

        Console.WriteLine($"End of {nameof(StartTrackingApp)}.");
    }

    private void OnTimerElapsed(object? sender, int e)
    {
       // StartTrackingApp();
    }
}
