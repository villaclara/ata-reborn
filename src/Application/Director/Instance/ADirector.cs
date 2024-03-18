using Application.Common.Abstracts;
using Application.Models;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Director.Instance;

/// <summary>
/// Abstract Director class for managing all the work done by tracking applications.
/// Any Director should inherit from this class.
/// </summary>
public abstract class ADirector
{
	/// <summary>
	/// Launch the Director. Automatically perform tracking and all stuff.
	/// </summary>
	public abstract void Run();

	/// <summary>
	/// Launch the Director and perform tracking only ONCE.
	/// </summary>
	public abstract void RunOnceManually();

	/// <summary>
	/// Event is raised when Director has done the full one cycle of work.
	/// Subscribe to it if you want to have actual values of data.
	/// </summary>
	public abstract event EventHandler? WorkDone;

	/// <summary>
	/// Instance of <see cref="IDataIssuer"/> used to retrieve <see cref="AppInstanceVM"/> objects.
	/// </summary>
	public abstract IDataIssuer DataIssuer { get; set; }

	/// <summary>
	/// Add the application to be tracked.
	/// </summary>
	public abstract void AddAppToTrackedList(string processName, string? appName = null);

	/// <summary>
	/// Remove the application from tracking.
	/// </summary>
	public abstract void RemoveAppFromTrackedList(string processName);

	
}
