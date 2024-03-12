using Application.Models;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilities;

public static class MyMapService
{
	public static TDest? Map<TSource, TDest>(TSource? source) 
		where TDest : class 
		where TSource : class
	{
		if(source == null)
		{
			return null;
		}

		// map Model to VM
		if(source is AppInstance fromModel)
		{
			var to = new AppInstanceVM()
			{
				Name = fromModel.Name,
				IsRunning = fromModel.IsRunning,
				CurrentSessionTime = fromModel.CurrentSessionTime,
				LastRunningDate = fromModel.LastRunningDate,
				UpTimeList = fromModel.UpTimes.ToList()
			};

			return to as TDest;
		}

		// map VM to Model
		if(source is AppInstanceVM fromVM)
		{
			var to = new AppInstance()
			{
				Name = fromVM.Name,
				IsRunning = fromVM.IsRunning,
				CurrentSessionTime = fromVM.CurrentSessionTime,
				LastRunningDate = fromVM.LastRunningDate,
				UpTimes = fromVM.UpTimeList
			};

			return to as TDest;
		}

		// map failed
		return null;

	}
}
