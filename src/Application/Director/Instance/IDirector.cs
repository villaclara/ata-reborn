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
	IReadData ReadDataService { get; set; }
	IWriteData WriteDataService { get; set; }

	List<AppInstance> Apps { get; }
	List<IAppHandler> Handlers { get; }

	StaticTimerService Timer { get; }

	/// <summary>
	/// Launch the Director. Automatically perform tracking and all stuff.
	/// </summary>
	Task RunAsync();

	/// <summary>
	/// Launch the Director and perform tracking only ONCE.
	/// </summary>
	Task RunOnceManuallyAsync();

	/// <summary>
	/// Event is raised when Director has done the full one cycle of work.
	/// Subscribe to it if you want to have actual values of data.
	/// </summary>
	event Func<object, int, Task>? WorkDone;

	Task OnTimerElapsed(object? sender, int minutes);

	/// <summary>
	/// Add the application to be tracked.
	/// </summary>
	void AddAppToTrackedList(string processName, string? appName = null);

	/// <summary>
	/// Remove the application from tracking.
	/// </summary>
	void RemoveAppFromTrackedList(string processName);
}
