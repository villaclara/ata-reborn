using Application.AppToTrack.Abstracts;
using Application.Common.Abstracts;
using Application.Common.Timers;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Director.Instance;

public interface IDirector
{
	/// <summary>
	/// Service to read data from source and return List of <see cref="AppInstance"/>.
	/// </summary>
	IReadData ReadDataService { get; set; }

	/// <summary>
	/// Service to Write the data (List of <see cref="AppInstance"/>) to the destination.
	/// </summary>
	IWriteData WriteDataService { get; set; }

	/// <summary>
	/// List of <see cref="AppInstance"/> objects being tracked.
	/// </summary>
	List<AppInstance> Apps { get; }

	/// <summary>
	/// List of <see cref="IAppHandler"/> objects which perform tracking of each related <see cref="AppInstance"/> object.
	/// </summary>
	List<IAppHandler> Handlers { get; }

	/// <summary>
	/// Timer for triggering tracking.
	/// </summary>
	StaticTimerService Timer { get; }

	/// <summary>
	/// Timer related method. Is called when <see cref="Timer"/> has elapsed. Need to Subscribe it to Timer.TimeElapsed.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="minutes"></param>
	/// <returns></returns>
	Task OnTimerElapsed(object? sender, int minutes);

	/// <summary>
	/// Launch the Director. Automatically perform tracking and all stuff.
	/// </summary>
	Task RunAsync();

	/// <summary>
	/// Launch the Director and perform tracking only ONCE.
	/// </summary>
	Task RunOnceManuallyAsync();

	/// <summary>
	/// Add the application to be tracked.
	/// </summary>
	void AddAppToTrackedList(string processName, string? appName = null);

	/// <summary>
	/// Remove the application from tracking.
	/// </summary>
	void RemoveAppFromTrackedList(string appName, string? processName = null);

	/// <summary>
	/// Event is raised when Director has done the full one cycle of work.
	/// Subscribe to it if you want to have actual values of data.
	/// </summary>
	event Func<object, int, Task>? WorkDone;
}
