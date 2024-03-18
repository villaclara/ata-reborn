using Application.AppToTrack.Abstracts;
using Application.AppToTrack.Interactors;
using Application.AppToTrack.Services;
using Application.Common.Abstracts;
using Application.Common.Services;
using Application.Common.Timers;
using Application.Director.Instance;
using Application.Models;
using Application.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Director.Creation;

public class DefaultDirector : ADirector
{
	internal DefaultDirector()
	{

		_readService = new ReadDataAsStringFromFile();
		_writeService = new WriteDataStringToFile();

		string data = _readService.RetrieveData()!;
		_apps = AppsJsonStringConverter.ConvertJsonToApps(data);

		foreach(var app in _apps)
		{
			_handlers.Add(new AppHandler(new Interactor(app)));
		}

		DataIssuer = new DataIssuer(_readService);

	
		_timer = StaticTimerService.GetInstance();
		_timer.TimeElapsed -= OnTimerElapsed;
		_timer.TimeElapsed += OnTimerElapsed;
	}

	private readonly IReadData<string> _readService;
	private readonly IWriteData<string> _writeService;


	private readonly StaticTimerService _timer;

	public IReadData<string> ReadService { get; init; } = null!;
	public IWriteData<string> WriteData { get; init; } = null!;

	private List<AppInstance> _apps = [];
	private List<IAppHandler> _handlers = [];

	public override IDataIssuer DataIssuer { get; set; }

	public override event Func<object, int, Task>? WorkDone;

	public override void AddAppToTrackedList(string processName, string? appName = null)
	{
		Log.Information("{@Method} - started with parameters - processName - {@processName}, appName - {@appName}", nameof(AddAppToTrackedList), processName, appName);

		var app = AppInstanceCreator.CreateAppInstanceToTrack(appName, processName);
		Log.Information("{@Method} - AppInstance was created with processname - {@app}", nameof(AddAppToTrackedList), app);

		IInteractor interactor = new Interactor(app);
		IAppHandler appHandler = new AppHandler(interactor);
		_apps.Add(app);
		Log.Information("{@Method} - App {@App} was added to list. List of apps count - {@count}", nameof(AddAppToTrackedList), app, _apps.Count);
		_handlers.Add(appHandler);
		Log.Information("{@Method} - Handler {@Handler} was added to list. List of handlers count - {@count}", nameof(AddAppToTrackedList), appHandler, _handlers.Count);

	}

	public override void RemoveAppFromTrackedList(string processName)
	{
		throw new NotImplementedException();
	}

	public override async Task RunAsync() =>
		await OnTimerElapsed(this, ConstantValues.TIMER_INTERVAL_MS);
	

	public override async Task RunOnceManuallyAsync() =>
		await DoWork();

	private async Task DoWork()
	{
		Log.Information("\n");
		Log.Information("\n");
		Log.Information("{@Method} - started.", nameof(DoWork));

		if (_handlers.Count == 0)
		{
			Log.Warning("{@Method} - {@List} of handlers is empty, nothing to do", nameof(OnTimerElapsed), nameof(List<AppHandler>));
			return;
		}


		// run task for each app handler
		var tasks = new List<Task>();
		foreach (var h in _handlers)
		{
			tasks.Add(Task.Run(h.StartTrackingApp));
		}

		await Task.WhenAll(tasks);

		string json = AppsJsonStringConverter.ConvertAppsToJson(_apps);
		bool result = _writeService.WriteToFile(json);

		Log.Information("{@Method} - end", nameof(OnTimerElapsed));
	}

	public override async Task OnTimerElapsed(object? sender, int minutes)
	{
		await DoWork();
		WorkDone?.Invoke(this, minutes);
	}
}
