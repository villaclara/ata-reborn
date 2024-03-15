using Application.AppToTrack.Interactors;
using Application.AppToTrack.Services;
using Application.Common.Abstracts;
using Application.Common.Timers;
using Application.Models;
using Application.Utilities;
using Serilog;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Application.Common.Services;

public class MainWorker 
{

	#region PUBLIC, visible in 3rd Apps

	public event Func<object, int, Task>? WorkDone;
	public void Run()
	{
		_timer.TimeElapsed += DoWork;
	}

	public IEnumerable<AppInstanceVM> GetAppsTrackedList()
	{
		throw new NotImplementedException();
	}

	#endregion

	public MainWorker()
	{
		// IT SHOULD BE MOVED INTO SEPARATE METHOD, NOT IN CTOR
		var readstring = ReadData.RetrieveData();
		_apps = AppsJsonStringConverter.ConvertJsonToApps(readstring!);

		foreach (var appInstance in _apps)
		{
			IInteractor interactor = new Interactor(appInstance);
			_handlers.Add(new AppHandler(interactor));
		}

	}

	#region PRIVATE

	private StaticTimerService _timer = StaticTimerService.GetInstance();
	
	private List<AppInstance> _apps = new List<AppInstance>();
	private List<AppHandler> _handlers = new List<AppHandler>();
	public IReadData<string> ReadData { get; init; } = new ReadDataAsStringFromFile();
	public IWriteData<string> WriteData { get; init; } = new WriteDataStringToFile();

	public async Task DoWork(object? sender, int value)
	{
		Log.Information("Method DOWORK called.");

		
		if (_handlers.Count == 0)
		{
			Log.Warning("{@Method} - {@List} of handlers is empty, nothing to do", nameof(DoWork), nameof(List<AppHandler>));
			return;
		}


		// run task for each app handler
		var tasks = new List<Task>();
		foreach (var handler in _handlers)
		{
			tasks.Add(Task.Run(handler.StartTrackingApp));
		}

		await Task.WhenAll(tasks);

		string json = AppsJsonStringConverter.ConvertAppsToJson(_apps);
		bool result = WriteData.WriteToFile(json);

		Log.Information("{@Method} - end", nameof(DoWork));

		// calling self event to be raised 
		OnWorkDone();
	}

	private void OnWorkDone()
	{
		WorkDone?.Invoke(this, 1);
	}

	#endregion
}


public class MainWorkerBuilder
{
	private MainWorker _worker;

	public MainWorkerBuilder()
	{
		_worker = new MainWorker();
	}

	public MainWorkerBuilder SetReadService(IReadData<string> readData)
	{
		throw new NotImplementedException();
	}

	public MainWorkerBuilder WithReadService(Action<ReadServiceBuilder> readBuilder)
	{
		var rsb = new ReadServiceBuilder();
		readBuilder(rsb);
		return this;
	}

	public MainWorker Build()
	{
		return _worker;
	}
}

public class ReadServiceBuilder
{
	private IReadData<string> _readData;
	public ReadServiceBuilder WithReadString<T>(IReadData<T> readData)
	{
		_readData = readData as IReadData<string>;
		return this;
	}
}