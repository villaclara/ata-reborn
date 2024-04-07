using Application.Director.Instance;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.Commands;
using UI.WPF.Services;

namespace UI.WPF.ViewModels;

public partial class ToolbarViewModel : BaseViewModel
{

	private readonly IDirector _director;

	private readonly INavigationService _navigation;

	public ToolbarViewModel(IDirector director, INavigationService navigation)
	{
		_director = director;
		_navigation = navigation;
	}


	[RelayCommand]
	private void ShowProcessListScreen()
	{
		_navigation.NavigateTo<ProcessListViewModel>();
	}


	[RelayCommand]
	private void ShowHomeScreen()
	{
		_navigation.NavigateTo<TrackedAppsViewModel>();
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
