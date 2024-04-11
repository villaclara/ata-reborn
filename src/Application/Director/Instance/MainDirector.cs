using Application.AppToTrack.Abstracts;
using Application.AppToTrack.Interactors;
using Application.AppToTrack.Services;
using Application.Common.Abstracts;
using Application.Common.Services;
using Application.Common.Timers;
using Application.Models;
using Application.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Director.Instance;

public class MainDirector : IDirector
{
	internal MainDirector()
	{
		Apps = [];
		Handlers = [];


		// setting default values to prevent null reference
		// but these should be set via Builder
		ReadDataService = new ReadDataFromJsonFile();
		WriteDataService = new WriteDataStringToFile();
	}

	private Task Timer_TimeElapsed(object arg1, int arg2)
	{
		throw new NotImplementedException();
	}

	public IReadData ReadDataService { get; set; } = null!;
	public IWriteData WriteDataService { get; set; } = null!;

	public List<AppInstance> Apps { get; } = null!;

	public List<IAppHandler> Handlers { get; } = null!;

	public StaticTimerService Timer { get; private set; }

	public event Func<object, int, Task>? WorkDone;

	public void AddAppToTrackedList(string processName, string? appName = null)
	{
		Log.Information("{@Method} - started with parameters - processName - {@processName}, appName - {@appName}", nameof(AddAppToTrackedList), processName, appName);

		var app = AppInstanceCreator.CreateAppInstanceToTrack(appName, processName);
		Log.Information("{@Method} - AppInstance was created with processname - {@app}", nameof(AddAppToTrackedList), app);

		IInteractor interactor = new Interactor(app);
		IAppHandler appHandler = new AppHandler(interactor);
		Apps.Add(app);
		Log.Information("{@Method} - App {@App} was added to list. List of apps count - {@count}", nameof(AddAppToTrackedList), app, Apps.Count);
		Handlers.Add(appHandler);
		Log.Information("{@Method} - Handler {@Handler} was added to list. List of handlers count - {@count}", nameof(AddAppToTrackedList), appHandler, Handlers.Count);
	}

	public async Task OnTimerElapsed(object? sender, int minutes)
	{
		await DoWork();
		WorkDone?.Invoke(this, minutes);
	}

	public void RemoveAppFromTrackedList(string appName, string? processName = null)
	{
		// try by processName at first, however if it is mising then by appName.
		var app = processName != null 
			? Apps.Where(a => a.ProcessNameInOS == processName).FirstOrDefault() 
			: Apps.Where(a => a.ProcessNameInOS == appName).FirstOrDefault();

		if (app != null)
		{
			var index = Apps.IndexOf(app);
			Apps.RemoveAt(index);
			Handlers.RemoveAt(index);
		}
	}

	public async Task RunAsync()
	{
		Log.Information("{@Method} - method started.", nameof(RunAsync));
		var apps = ReadDataService.RetrieveData();
		Log.Information("{@Method} - read apps count - {@count}.", nameof(RunAsync), apps.Count);
		foreach(var app in apps)
		{
			// when first running reset the CurrentSessionTime for each app. 
			// Not ideal, but works for now.
			app.CurrentSessionTime = 0;
			Apps.Add(app);
			Handlers.Add(new AppHandler(new Interactor(app)));

			Log.Information("{@Method} - App ({@app}) was added to ({@Apps}) and ({@Handlers})", nameof(RunAsync), app.ProcessNameInOS, nameof(Apps), nameof(Handlers));
		}

		Timer = StaticTimerService.GetInstance();
		Timer.TimeElapsed -= OnTimerElapsed;
		Timer.TimeElapsed += OnTimerElapsed;

		await OnTimerElapsed(this, ConstantValues.TIMER_INTERVAL_MS);
	}

	public async Task RunOnceManuallyAsync()
	{
		Log.Information("{@Method} - ({@Director}) Method Started.", nameof(RunOnceManuallyAsync), typeof(MainDirector));
		await DoWork();

	}

	private async Task DoWork()
	{
		Log.Information("\n");
		Log.Information("\n");
		Log.Information("{@Method} - started.", nameof(DoWork));

		if (Handlers.Count == 0)
		{
			Log.Warning("{@Method} - {@List} of handlers is empty, nothing to do", nameof(DoWork), nameof(List<AppHandler>));
			return;
		}


		// run task for each app handler
		var tasks = new List<Task>();
		foreach (var h in Handlers)
		{
			tasks.Add(Task.Run(h.StartTrackingApp));
		}

		await Task.WhenAll(tasks);

		string json = AppsJsonStringConverter.ConvertAppsToJson(Apps);
		bool result = WriteDataService.WriteData(Apps);

		Log.Information("{@Method} - end", nameof(DoWork));
	}
}
