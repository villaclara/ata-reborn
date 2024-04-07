using Application.Common.Abstracts;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.ViewModels;

public class ProcessListViewModel : BaseViewModel
{
	private readonly IGetProcs _getProcessesService;

	public ProcessListViewModel(IGetProcs getProcessesService)
	{
		_getProcessesService = getProcessesService;
		ProcessesList = _getProcessesService.GetUniqueProcessesAsList(useSystemManagement: false).OrderBy(p => p.ProcessName);
	}

	public IEnumerable<UniqueProcess> ProcessesList { get; }



}
