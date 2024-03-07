using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

/// <summary>
/// Represents the Application to keep track time of it.
/// </summary>
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
	/// DateTime object when the AppInstance was running last.
	/// </summary>
	public DateTime LastRunningDate {  get; set; }

	/// <summary>
	/// Time in minutes for the current active session. If the App is not Running the time is 0.
	/// </summary>
	public int CurrentSessionTime {  get; set; }

	/// <summary>
	/// List of <see cref="UpTime"/> objects that represent active AppInstance time for each date.
	/// </summary>
	public IList<UpTime> UpTimes { get; set; } = new List<UpTime>();

	/// <summary>
	/// Latest Date when AppInstance check was performed.
	/// </summary>
	public DateTime LastCheckedDate { get; set; }
	
	/// <summary>
	/// Latest Date when UpTimes were updated.
	/// </summary>
	public DateTime LastUpdatedUpTimesDate { get; set; }
}
