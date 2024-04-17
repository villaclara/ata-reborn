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
using UI.WPF.Services.Abstracts;

namespace UI.WPF.ViewModels;

public class TrackedAppsViewModel : BaseViewModel, IRecipient<TrackedAppAddedMessage>, IRecipient<TrackedAppDeletedMessage>
{
	public ObservableCollection<TrackedAppItemViewModel> AppItems { get; }

	private readonly IDirector _director;
	private readonly IDataIssuer _dataIssuer;
	private readonly ICustomDialogService _customDialog;

    public TrackedAppsViewModel(IDirector director, ICustomDialogService customDialog)
    {
		Log.Information("{@Method} - Start constructor.", nameof(TrackedAppItemViewModel));
		
		try
		{
			StrongReferenceMessenger.Default.Register<TrackedAppAddedMessage>(this);
			StrongReferenceMessenger.Default.Register<TrackedAppDeletedMessage>(this);
			Log.Information("{@Method} - Registered ({@Message}) will be received in ({@type}).", nameof(TrackedAppItemViewModel), nameof(TrackedAppAddedMessage), typeof(TrackedAppItemViewModel));
		}
		catch(Exception ex)
		{
			Log.Error("{@Method} - Error when registering ({@Message}) - {@Error}.", nameof(TrackedAppItemViewModel), nameof(TrackedAppAddedMessage), ex.Message);
		}

		AppItems = [];
		_director = director;
		_customDialog = customDialog;
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
				TrackedAppItemViewModel vm = new TrackedAppItemViewModel(appVM, _dataIssuer, _customDialog);
				AppItems.Add(vm);

				// Event to update values in the UI
				_director.WorkDone -= vm.Director_WorkDone;
				_director.WorkDone += vm.Director_WorkDone;

				Log.Information("{@Method} - Created VM ({@vm}) for ({@app}).", nameof(SetUpAppItems), nameof(vm), app.ProcessNameInOS);
				Log.Information("{@Method} - {@AppItems} count - {@count}.", nameof(SetUpAppItems), nameof(AppItems), AppItems.Count);
			}

		}
	}


	public async void Receive(TrackedAppAddedMessage message)
	{
		try
		{
			_director.AddAppToTrackedList(message.ProcessName, message.AppName ?? null);

			var added = MyMapService.Map<AppInstance, AppInstanceVM>(_director.Apps.Last());
			TrackedAppItemViewModel vm = new(added!, _dataIssuer, _customDialog);
			AppItems.Add(vm);

			_director.WorkDone -= vm.Director_WorkDone;
			_director.WorkDone += vm.Director_WorkDone;

			Log.Information("{@Method} - ({@App}) was added to ({@director}) and ({@AppItems}).", nameof(Receive), added?.Name, nameof(_director), nameof(AppItems));
			await _director.RunOnceManuallyAsync();
			//await vm.Director_WorkDone(new object(), 0);
		}
		catch(Exception ex)
		{
			Log.Error("{@Method} - {@ex}.", nameof(Receive), ex.Message);
		}

	}

	public void Receive(TrackedAppDeletedMessage message)
	{
		try
		{
			var appvm = AppItems.Where(item => item.AppName == message.AppName).FirstOrDefault();
			if(appvm != null)
			{
				_director.RemoveAppFromTrackedList(message.AppName);
				_director.WorkDone -= appvm.Director_WorkDone;
				AppItems.Remove(appvm);
				_director.RunOnceManuallyAsync();
			}

			Log.Information("{@Method} - ({@App}) was removed from ({@director}) and ({@AppItems}).", nameof(Receive), message.AppName, nameof(_director), nameof(AppItems));

		}
		catch (Exception ex)
		{
			Log.Error("{@MEthod} - {@ex}.", nameof(Receive), ex.Message);
		}
	}
}
