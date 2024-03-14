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

	public int GetCurrentSessionTime(bool isRunnnig)
	{
		return isRunnnig switch
		{
			true => AppInteractor.GetAppInstace().CurrentSessionTime,
			false => 0
		};
	}

	public int GetSessionTimeForDate(DateOnly date)
	{
		var times = AppInteractor.GetAppInstace().UpTimes;
		
		var timedate = times.Where(t => t.Date == date).FirstOrDefault();

		return timedate == null ? 0 : timedate.Minutes;
	}

	public void UpdateTimeValues()
	{
		
	}


	public void TrackTime()
	{
		var trackedApp = AppInteractor.GetAppInstace();

		// If the App is not running we set current session time and return back;
		if(!trackedApp.IsRunning)
		{
			trackedApp.CurrentSessionTime = 0;
			return;
		}

		// Adding time to Current Session
		trackedApp.CurrentSessionTime += ConstantValues.TIMER_INTERVAL_M;

		Log.Information("{@App} - {@Method} - {@CurrentSessionTime} - {@Value}", trackedApp.ProcessNameInOS, nameof(TrackTime), nameof(trackedApp.CurrentSessionTime), trackedApp.CurrentSessionTime);


	   var dateNow = DateOnly.FromDateTime(DateTime.Now);

		// Check if Today date is present in list
		var today = trackedApp.UpTimes.Where(t => t.Date == dateNow).FirstOrDefault();

		// If yes - We add one minute
		if (today != null)
		{
			today.Minutes += ConstantValues.TIMER_INTERVAL_M;
		}
		// if no - We add new object of UpTimes with minute
		else
		{
			trackedApp.UpTimes.Add(new Shared.Models.UpTime()
			{
				Date = dateNow,
				Minutes = ConstantValues.TIMER_INTERVAL_M
			});
		}

		// add the time that we changed upTimes
		trackedApp.LastUpdatedUpTimesDate = DateTime.Now;

		Log.Information("{@App} - {@Method} - {@MinutesToday} - {@Date}", trackedApp.ProcessNameInOS, nameof(TrackTime), nameof(today), today);
		Log.Information("{@App} - {@Method} - {@LastUpdatedTime} - {@Value}", trackedApp.ProcessNameInOS, nameof(TrackTime), nameof(trackedApp.LastUpdatedUpTimesDate), trackedApp.LastUpdatedUpTimesDate);

	}
}
