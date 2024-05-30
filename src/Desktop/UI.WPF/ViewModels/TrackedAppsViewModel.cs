using Application.Common.Abstracts;
using Application.Common.Services;
using Application.Director.Instance;
using Application.Models;
using Application.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Serilog;
using Shared.ViewModels;
using System.Collections.ObjectModel;
using UI.WPF.Services;
using UI.WPF.Services.Abstracts;

namespace UI.WPF.ViewModels;

/// <summary>
/// Container, keeps and handles <see cref="TrackedAppItemViewModel"/> collection.
/// </summary>
public partial class TrackedAppsViewModel : BaseViewModel, IRecipient<TrackedAppAddedMessage>, IRecipient<TrackedAppDeletedMessage>
{
	public ObservableCollection<TrackedAppItemViewModel> AppItems { get; }

	private readonly IDirector _director;
	private readonly IDataIssuer _dataIssuer;
	private readonly ICustomDialogService _customDialog;
	private readonly IRetrieveChartService _retrieveChart;

	// Property for displaying 'Empty Trakced Apps Text'. Visible if AppItems.Count == 0.
	[ObservableProperty]
	private string _defaultTextVisibility = "Visible";


	public TrackedAppsViewModel(IDirector director, ICustomDialogService customDialog, IRetrieveChartService retrieveChart)
	{
		Log.Information("{@Method} - Start constructor.", nameof(TrackedAppItemViewModel));

		try
		{
			StrongReferenceMessenger.Default.Register<TrackedAppAddedMessage>(this);
			StrongReferenceMessenger.Default.Register<TrackedAppDeletedMessage>(this);
			Log.Information("{@Method} - Registered ({@Message}) will be received in ({@type}).", nameof(TrackedAppItemViewModel), nameof(TrackedAppAddedMessage), typeof(TrackedAppItemViewModel));
		}
		catch (Exception ex)
		{
			Log.Error("{@Method} - Error when registering ({@Message}) - {@Error}.", nameof(TrackedAppItemViewModel), nameof(TrackedAppAddedMessage), ex.Message);
		}

		AppItems = [];
		_director = director;
		_customDialog = customDialog;
		_retrieveChart = retrieveChart;
		_dataIssuer = new DataIssuer(new ReadDataFromJsonFile());
		Log.Information("{@Method} - Data issuer created - {@dataissuer}", nameof(TrackedAppItemViewModel), _dataIssuer);

		CreateVMsForTrackedApplications();
	}


	private void CreateVMsForTrackedApplications()
	{
		if (_director.Apps.Count != 0)
		{
			DefaultTextVisibility = "Hidden";
		}

		foreach (var app in _director.Apps)
		{
			var appVM = MyMapService.Map<AppInstance, AppInstanceVM>(app);

			if (appVM != null)
			{
				TrackedAppItemViewModel vm = new TrackedAppItemViewModel(app: appVM, dataIssuer: _dataIssuer, customDialog: _customDialog, retrieveChartService: _retrieveChart);
				AppItems.Add(vm);

				// Event to update values in the UI
				_director.WorkDone -= vm.TrackedAppItemVM_Director_WorkDone;
				_director.WorkDone += vm.TrackedAppItemVM_Director_WorkDone;

				Log.Information("{@Method} - Created VM ({@vm}) for ({@app}).", nameof(CreateVMsForTrackedApplications), nameof(vm), app.ProcessNameInOS);
				Log.Information("{@Method} - {@AppItems} count - {@count}.", nameof(CreateVMsForTrackedApplications), nameof(AppItems), AppItems.Count);
			}

		}
	}


	public async void Receive(TrackedAppAddedMessage message)
	{
		try
		{
			_director.AddAppToTrackedList(message.ProcessName, message.AppName ?? null);

			var added = MyMapService.Map<AppInstance, AppInstanceVM>(_director.Apps.Last());
			TrackedAppItemViewModel vm = new(added!, _dataIssuer, _customDialog, _retrieveChart);
			AppItems.Add(vm);

			_director.WorkDone -= vm.TrackedAppItemVM_Director_WorkDone;
			_director.WorkDone += vm.TrackedAppItemVM_Director_WorkDone;

			Log.Information("{@Method} - ({@App}) was added to ({@director}) and ({@AppItems}).", nameof(Receive), added?.Name, nameof(_director), nameof(AppItems));
			await _director.RunOnceManuallyAsync();

			// Set the Default text to be hidden as we have at least one app
			DefaultTextVisibility = "Hidden";

		}
		catch (Exception ex)
		{
			Log.Error("{@Method} - {@ex}.", nameof(Receive), ex.Message);
		}

	}

	// Removing Application from tracking and removing TrackedAppItemView.
	public void Receive(TrackedAppDeletedMessage message)
	{
		try
		{
			var appvm = AppItems.Where(item => item.AppName == message.AppName).FirstOrDefault();
			if (appvm != null)
			{
				_director.RemoveAppFromTrackedList(message.AppName);
				_director.WorkDone -= appvm.TrackedAppItemVM_Director_WorkDone;
				AppItems.Remove(appvm);
				_director.RunOnceManuallyAsync();
			}

			Log.Information("{@Method} - ({@App}) was removed from ({@director}) and ({@AppItems}).", nameof(Receive), message.AppName, nameof(_director), nameof(AppItems));

			// Set the Default text to be visible
			if (_director.Apps.Count == 0)
			{
				DefaultTextVisibility = "Visible";
			}
		}
		catch (Exception ex)
		{
			Log.Error("{@MEthod} - {@ex}.", nameof(Receive), ex.Message);
		}
	}
}
