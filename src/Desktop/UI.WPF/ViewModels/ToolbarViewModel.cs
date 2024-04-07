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

	private readonly IDirector _director;

	public ToolbarViewModel(IDirector director)
	{
		_director = director;
	}


	[RelayCommand]
	private void ShowProcessListScreen()
	{
		_director.AddAppToTrackedList("notepad");
	}


	[RelayCommand]
	private void ShowHomeScreen()
	{

	}

	[RelayCommand]
	private void ShowSettingsScreen()
	{

	}

	[RelayCommand]
	private void ToggleDayNightTheme()
	{

	}


	

}
