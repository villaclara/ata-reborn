using Application.Common.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Serilog;
using Shared.Models;
using UI.WPF.Services;
using UI.WPF.Services.Abstracts;

namespace UI.WPF.ViewModels;

public partial class ProcessListViewModel : BaseViewModel
{
	public ProcessListViewModel(IGetProcs getProcessesService, INavigationService navigation)
	{
		_navigation = navigation;
		_getProcessesService = getProcessesService;
		//ProcessesList = _getProcessesService.GetUniqueProcessesAsList(useSystemManagement: false).OrderBy(p => p.ProcessName);

		_processesList = [];

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
		LoadDataAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
	}


	private readonly IGetProcs _getProcessesService;
	private readonly INavigationService _navigation;

	[ObservableProperty]
	public IEnumerable<UniqueProcess> _processesList;

	[ObservableProperty]
	public UniqueProcess? _selectedProcess;

	[RelayCommand]
	public void AddSelectedAppToTrack(UniqueProcess selectedProcess)
	{
		try
		{
			StrongReferenceMessenger.Default.Send(new TrackedAppAddedMessage(selectedProcess.ProcessName, selectedProcess.AppName ?? null));
			Log.Information("{@Method} - ({@Message}) with AppName({@AppName}) and ProcName({@Proc}) was sent.", nameof(AddSelectedAppToTrack), nameof(TrackedAppAddedMessage), selectedProcess.AppName, selectedProcess.ProcessName);
		}
		catch (Exception ex)
		{
			Log.Error("{@Method} - Exception ({@ex}) when adding ({@app}). App was not added.", nameof(AddSelectedAppToTrack), ex.Message, selectedProcess?.ProcessName);
		}
		finally
		{
			_navigation.NavigateTo<TrackedAppsViewModel>();
		}

	}

	public async Task LoadDataAsync()
	{
		var procs = await _getProcessesService.GetUniqueProcessesV2Async();
		ProcessesList = procs.OrderBy(p => p.ProcessName);
	}
}


