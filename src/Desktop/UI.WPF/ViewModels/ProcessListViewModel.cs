using Application.Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.ViewModels;

public class ProcessListViewModel
{
	private readonly IGetProcs _getProcessesService;

	public ProcessListViewModel(IGetProcs getProcessesService)
	{
		_getProcessesService = getProcessesService;
		ProcessesList = _getProcessesService.GetUniqueProcesses(useSystemManagement: true);
	}

	public IDictionary<string, string> ProcessesList { get; }


}
