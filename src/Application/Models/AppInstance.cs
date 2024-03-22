using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models;

public class AppInstance
{
	/// <summary>
	/// Name displayed in the UI.
	/// </summary>
	public string Name { get; set; } = null!;

	/// <summary>
	/// Name of the process in the OS. 
	/// </summary>
	public string ProcessNameInOS { get; set; } = null!;

	/// <summary>
	/// Shows if the <see cref="AppInstance"/> is currently running.
	/// </summary>
	public bool IsRunning { get; set; }

	/// <summary>
	/// The DateTime when the AppInstance was created
	/// </summary>
	public DateTime CreatedAt {  get; set; }

	/// <summary>
	/// DateTime object when the AppInstance was running last.
	/// </summary>
	public DateTime LastRunningDate { get; set; }

	/// <summary>
	/// Time in minutes for the current active session. If the App is not Running the time is 0.
	/// </summary>
	public double CurrentSessionTime { get; set; }

	/// <summary>
	/// List of <see cref="UpTime"/> objects that represent active AppInstance time for each date.
	/// </summary>
	public IList<UpTime> UpTimes { get; set; } = new List<UpTime>();

	/// <summary>
	/// Latest Date when AppInstance check was performed.
	/// </summary>
	public DateTime LastStateCheckedDate { get; set; }

	/// <summary>
	/// Latest Date when ITimeTracker was invoked. Used in checking time difference.
	/// </summary>
	public DateTime LastTimeTrackedDate { get; set; }

	/// <summary>
	/// Latest Date when UpTimes were updated.
	/// </summary>
	public DateTime LastUpdatedUpTimesDate { get; set; }

	// preventing creating objects from other apps
	internal AppInstance() { }


	// Using when mapping from JSON string read from file
	[JsonConstructor]
	private AppInstance(string Name, string ProcessNameInOS, bool IsRunning, DateTime CreatedAt, 
		DateTime LastRunningDate, double CurrentSessionTime, IList<UpTime> UpTimes, DateTime LastStateCheckedDate, DateTime LastTimeTrackedDate, DateTime LastUpdatedUpTimesDate)
	{
		this.Name = Name;
		this.ProcessNameInOS = ProcessNameInOS;
		this.IsRunning = IsRunning;
		this.CreatedAt = CreatedAt;
		this.LastRunningDate = LastRunningDate;
		this.CurrentSessionTime = CurrentSessionTime;
		this.UpTimes = UpTimes;
		this.LastStateCheckedDate = LastStateCheckedDate;
		this.LastTimeTrackedDate = LastTimeTrackedDate;
		this.LastUpdatedUpTimesDate = LastUpdatedUpTimesDate;
		
	}
}
