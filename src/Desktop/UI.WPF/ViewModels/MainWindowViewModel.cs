using Application.Common.Services;
using Application.Director.Creation;
using Application.Director.Instance;
using Application.Models;
using Application.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using Serilog;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.Components;
using UI.WPF.Services;

namespace UI.WPF.ViewModels;

public partial class MainWindowViewModel: ObservableObject
{
	public MainWindowViewModel(INavigationService navigation, ToolbarViewModel toolbarViewModel, TrackedAppsViewModel trackedAppsViewModel, IDirector director)
	{
		_navigation = navigation;
		_director = director;
		_director.WorkDone += _director_WorkDone;

		ToolbarViewModel = toolbarViewModel;
		TrackedAppsViewModel = trackedAppsViewModel;
	}

	private Task _director_WorkDone(object arg1, int arg2)
	{
		LastDirectorWorkDone = _director.LastWorkDoneDate;
		return Task.CompletedTask;
	}


	// For Binding purposes.
	public ToolbarViewModel ToolbarViewModel { get; }
	public TrackedAppsViewModel TrackedAppsViewModel { get; } 


	// Is taking part in Binging Current View.
	public INavigationService Navigation => _navigation;
	private readonly INavigationService _navigation;
	private readonly IDirector _director;

	[ObservableProperty]
	private DateTime _lastDirectorWorkDone;

}
