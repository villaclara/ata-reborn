using Application.AppToTrack.Interactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppToTrack.Abstracts;

/// <summary>
/// Interface used to track and set the time of App running in AppInstance.
/// </summary>
public interface IAppTimeTracker
{
	IInteractor AppInteractor { get; }


	/// <summary>
	/// Get the AppInstance Uptime in the current session.
	/// </summary>
	/// <param name="isRunnnig">Indicates whether the appInstance is running at the moment.</param>
	/// <returns>Current Session Time in Minutes</returns>
	double GetCurrentSessionTime(bool isRunnnig);

	/// <summary>
	/// Get the total AppInstance uptime for selected Date.
	/// </summary>
	/// <param name="date">Date value for which to uptime to return.</param>
	/// <returns>Uptime in minutes.</returns>
	double GetSessionTimeForDate(DateOnly date);

	/// <summary>
	/// Updates the values in the AppInstance via interactor.
	/// </summary>
	void UpdateTimeValues();

	/// <summary>
	/// Is Called after the timer expires and automatically trackes time and updates values.
	/// </summary>
	void TrackTime();
}
