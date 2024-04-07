using Application.Common.Services;
using Application.Director.Instance;
using Application.Models;
using Application.Utilities;
using Serilog;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.ViewModels;

public class TrackedAppsViewModel : BaseViewModel
{
	public ObservableCollection<TrackedAppItemViewModel> AppItems { get; }

	private readonly IDirector _director;

    public TrackedAppsViewModel(IDirector director)
    {
		Log.Information("{@Method} - Start constructor.", nameof(TrackedAppItemViewModel));
		AppItems = [];

		_director = director;


		var dataIssuer = new DataIssuer(new ReadDataFromJsonFile());
		Log.Information("{@Method} - Data issuer created - {@dataissuer}", nameof(TrackedAppItemViewModel), dataIssuer);

		foreach (var app in director.Apps)
		{
			var appVM = MyMapService.Map<AppInstance, AppInstanceVM>(app);

			if (appVM != null)
			{
				TrackedAppItemViewModel vm = new TrackedAppItemViewModel(appVM, dataIssuer);
				director.WorkDone += vm.Director_WorkDone;
				AppItems.Add(vm);

				Log.Information("{@Method} - Created VM ({@vm}) for ({@app}).", nameof(TrackedAppItemViewModel), nameof(vm), app.ProcessNameInOS);
				Log.Information("{@Method} - {@AppItems} count - {@count}.", nameof(TrackedAppItemViewModel), nameof(AppItems), AppItems.Count);
			}


		

		}

	}
}
