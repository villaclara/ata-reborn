using Application.Common.Abstracts;
using Application.Common.Services;
using Application.Director.Instance;
using Application.Models;
using Application.Utilities;
using CommunityToolkit.Mvvm.Messaging;
using Serilog;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.Services;

namespace UI.WPF.ViewModels;

public class TrackedAppsViewModel : BaseViewModel, IRecipient<TrackedAppAddedMessage>
{
	public ObservableCollection<TrackedAppItemViewModel> AppItems { get; }

	private readonly IDirector _director;
	private readonly IDataIssuer _dataIssuer;

    public TrackedAppsViewModel(IDirector director)
    {
		Log.Information("{@Method} - Start constructor.", nameof(TrackedAppItemViewModel));
		
		try
		{
			StrongReferenceMessenger.Default.Register<TrackedAppAddedMessage>(this);
			Log.Information("{@Method} - Registered ({@Message}) will be received in ({@type}).", nameof(TrackedAppItemViewModel), nameof(TrackedAppAddedMessage), typeof(TrackedAppItemViewModel));
		}
		catch(Exception ex)
		{
			Log.Error("{@Method} - Error when registering ({@Message}) - {@Error}.", nameof(TrackedAppItemViewModel), nameof(TrackedAppAddedMessage), ex.Message);
		}

		AppItems = [];
		_director = director;
		_dataIssuer = new DataIssuer(new ReadDataFromJsonFile());
		Log.Information("{@Method} - Data issuer created - {@dataissuer}", nameof(TrackedAppItemViewModel), _dataIssuer);

		SetUpAppItems();
	}

	
	private void SetUpAppItems()
	{
		foreach (var app in _director.Apps)
		{
			var appVM = MyMapService.Map<AppInstance, AppInstanceVM>(app);

			if (appVM != null)
			{
				TrackedAppItemViewModel vm = new TrackedAppItemViewModel(appVM, _dataIssuer);
				AppItems.Add(vm);

				// Event to update values in the UI
				_director.WorkDone -= vm.Director_WorkDone;
				_director.WorkDone += vm.Director_WorkDone;

				Log.Information("{@Method} - Created VM ({@vm}) for ({@app}).", nameof(SetUpAppItems), nameof(vm), app.ProcessNameInOS);
				Log.Information("{@Method} - {@AppItems} count - {@count}.", nameof(SetUpAppItems), nameof(AppItems), AppItems.Count);
			}

		}
	}


	public void Receive(TrackedAppAddedMessage message)
	{
		AppItems.Clear();
		SetUpAppItems();
	}
}
