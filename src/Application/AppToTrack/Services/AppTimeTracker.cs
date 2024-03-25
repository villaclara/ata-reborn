using Application.AppToTrack.Abstracts;
using Application.AppToTrack.Interactors;
using Application.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppToTrack.Services;

public class AppTimeTracker(IInteractor interactor) : IAppTimeTracker
{
	public IInteractor AppInteractor { get; } = interactor;

	public double GetCurrentSessionTime(bool isRunnnig)
	{
		return isRunnnig switch
		{
			true => AppInteractor.GetAppInstace().CurrentSessionTime,
			false => 0
		};
	}

	public double GetSessionTimeForDate(DateOnly date)
	{
		var times = AppInteractor.GetAppInstace().UpTimes;

		var timedate = times.Where(t => t.Date == date).FirstOrDefault();

		return timedate == null ? 0 : timedate.Minutes;
	}

	public void UpdateTimeValues(TimeSpan timespanToAdd)
	{
		var trackedApp = AppInteractor.GetAppInstace();

		Log.Information("{@Method} - {@App} - {@CurrentSessionTime} - {@Value}",
				nameof(TrackTime), trackedApp.ProcessNameInOS, nameof(trackedApp.CurrentSessionTime), trackedApp.CurrentSessionTime);

		// Check how much seconds passed from last time tracked
		// Round it to 2 digits after comma.
		var timetoadd = Math.Round(timespanToAdd.TotalSeconds / 60, 2);

		Log.Information("{@Method} - Interval to add - {@Interval}",
			nameof(UpdateTimeValues), timetoadd);

		// Adding time to Current Session
		trackedApp.CurrentSessionTime += timetoadd;

		Log.Information("{@Method} - {@App} - {@CurrentSessionTime} - {@Value}",
			nameof(UpdateTimeValues), trackedApp.ProcessNameInOS, nameof(trackedApp.CurrentSessionTime), trackedApp.CurrentSessionTime);

		var dateNow = DateOnly.FromDateTime(DateTime.Now);

		// Check if Today date is present in list
		var today = trackedApp.UpTimes.Where(t => t.Date == dateNow).FirstOrDefault();

		// If yes - We add one minute
		if (today != null)
		{
			today.Minutes += timetoadd;
		}
		// if no - We add new object of UpTimes with minute
		else
		{
			trackedApp.UpTimes.Add(new Shared.Models.UpTime()
			{
				Date = dateNow,
				Minutes = timetoadd
			});
		}

		// add the time that we changed upTimes
		trackedApp.LastUpdatedUpTimesDate = DateTime.Now;

		Log.Information("{@Method} - {@App} - {@MinutesToday} - {@Date}", nameof(UpdateTimeValues), trackedApp.ProcessNameInOS, nameof(today), today);
		Log.Information("{@Method} - {@App} - {@LastUpdatedTime} - {@Value}", nameof(UpdateTimeValues), trackedApp.ProcessNameInOS, nameof(trackedApp.LastUpdatedUpTimesDate), trackedApp.LastUpdatedUpTimesDate);

	}


	public void TrackTime()
	{
		var trackedApp = AppInteractor.GetAppInstace();


		// If the App is not running we set current session time and return back;
		if (!trackedApp.IsRunning)
		{
			trackedApp.CurrentSessionTime = 0;
			return;
		}

		Log.Information("LTTD - {@1}, LCD - {@2}, LUD - {@3}, Now - {@f}", trackedApp.LastTimeTrackedDate, trackedApp.LastStateCheckedDate, trackedApp.LastUpdatedUpTimesDate, DateTime.Now);

		// Check if the AppInstance was nearly launched (for example 10 sec ago), then we do not want to add time to it.
		// We have LastTimeTrackedDate, which is set at the end of this method.
		var timeDifference = trackedApp.LastStateCheckedDate - trackedApp.LastTimeTrackedDate;
		if (timeDifference.TotalSeconds > ConstantValues.TIMER_INTERVAL_MS / 1000)
		{
			Log.Information("{@Method} - {@App} - LastCheckedDate ({@LCD}) minus LastTimeTrackedDate ({@LTTD}) is > Interval ({@Interval}). The appTime won't be updated this time.", 
				nameof(TrackTime), trackedApp.ProcessNameInOS, trackedApp.LastStateCheckedDate, trackedApp.LastTimeTrackedDate, ConstantValues.TIMER_INTERVAL_MS);
		}

		else
		{
			Log.Information("{@Method} - {@App} - LastCheckedDate ({@LCD}) minus LastTrackedTime ({@LTTD}) is < Interval ({@Interval}). The appTime will be updated this time.", 
				nameof(TrackTime), trackedApp.ProcessNameInOS, trackedApp.LastStateCheckedDate, trackedApp.LastTimeTrackedDate, ConstantValues.TIMER_INTERVAL_MS);


			UpdateTimeValues(timeDifference);
			
		}
		
		// add the time we Tracked appTime - performed this method
		trackedApp.LastTimeTrackedDate = DateTime.Now;
		Log.Information("{@Method} - {@App} - LastTimeTrackedDate - {@LastTimeTracked}", nameof(TrackTime), trackedApp.ProcessNameInOS, trackedApp.LastTimeTrackedDate);
	}
}
