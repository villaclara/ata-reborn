using Application.Common.Abstracts;
using Application.Director.Instance;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Serilog;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.Services;

namespace UI.WPF.ViewModels;

public partial class ProcessListViewModel : BaseViewModel
{
	public ProcessListViewModel(IDirector director, IGetProcs getProcessesService, INavigationService navigation)
	{
		_director = director;
		_navigation = navigation;
		_getProcessesService = getProcessesService;
		ProcessesList = _getProcessesService.GetUniqueProcessesAsList(useSystemManagement: false).OrderBy(p => p.ProcessName);
	}


	private readonly IDirector _director;
	private readonly IGetProcs _getProcessesService;
	private readonly INavigationService _navigation;

	public IEnumerable<UniqueProcess> ProcessesList { get; }

	[ObservableProperty]
	public UniqueProcess? _selectedProcess;

	[RelayCommand]
	public void AddSelectedAppToTrack(UniqueProcess selectedProcess)
	{
		try
		{
			Log.Information("{@Method} - Try block for adding ({@app}) by director.", nameof(AddSelectedAppToTrack), selectedProcess?.ProcessName);
			_director.AddAppToTrackedList(selectedProcess!.ProcessName, selectedProcess.AppName ?? null);
			StrongReferenceMessenger.Default.Send(new TrackedAppAddedMessage());
			Log.Information("{@Method} - ({@Message}) was sent to recepients.", nameof(AddSelectedAppToTrack), nameof(TrackedAppAddedMessage));
		}
		catch(Exception ex)
		{
			Log.Error("{@Method} - Exception ({@ex}) when adding ({@app}). App was not added.", nameof(AddSelectedAppToTrack), ex.Message, selectedProcess?.ProcessName);
		}
		finally
		{
			_navigation.NavigateTo<TrackedAppsViewModel>();
			this.Dispose();
		}

	}

	public void Dispose()
	{

	}
}


