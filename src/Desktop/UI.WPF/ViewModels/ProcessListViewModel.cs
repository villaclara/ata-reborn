using Application.Common.Abstracts;
using Application.Director.Instance;
using CommunityToolkit.Mvvm.Input;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.ViewModels;

public partial class ProcessListViewModel : BaseViewModel
{
	private readonly IDirector _director;
	private readonly IGetProcs _getProcessesService;

	public ProcessListViewModel(IDirector director, IGetProcs getProcessesService)
	{
		this._director = director;
		_getProcessesService = getProcessesService;
		ProcessesList = _getProcessesService.GetUniqueProcessesAsList(useSystemManagement: false).OrderBy(p => p.ProcessName);
	}

	public IEnumerable<UniqueProcess> ProcessesList { get; }



	[RelayCommand]
	public void AddSelectedAppToTrack()
	{
		_director.AddAppToTrackedList("");
	}

}
