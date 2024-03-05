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

	public int CurrentSessionTime {  get; set; }

	public IList<UpTime> UpTimes { get; set; } = new List<UpTime>();

}
