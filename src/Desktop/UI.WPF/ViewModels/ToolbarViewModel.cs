using Application.Director.Instance;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.Commands;

namespace UI.WPF.ViewModels;

public partial class ToolbarViewModel
{
	public IRelayCommand AddAppInstanceCommand { get; }

	public ToolbarViewModel(IDirector director)
	{
		AddAppInstanceCommand = new AddAppInstanceCommand(director);
	}


	[RelayCommand]
	private void DoCommand()
	{

	}

}
