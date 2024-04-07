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

public  class MainWindowViewModel : ObservableObject
{
	//public ObservableCollection<TrackedAppItemViewModel> AppItems { get; }
	public List<TrackedAppItemViewModel> AppItems { get; }


	private INavigationService _navigation; 


	public INavigationService Navigation { 
		get => _navigation; 
		set
		{
			_navigation = value;
			OnPropertyChanged();
		}
	}

	
	public ToolbarViewModel ToolbarViewModel { get; }

	public ProcessListViewModel ProcessListViewModel { get; }

	public TrackedAppsViewModel TrackedAppsViewModel { get; }


	private IDirector _director;

	public MainWindowViewModel(INavigationService navigation, IDirector director)
	{
		Log.Information("{@Method} - start.", nameof(MainWindowViewModel));
		
		
		Navigation = navigation;

		//AppItems = new ObservableCollection<TrackedAppItemViewModel>();
		AppItems = [];


		_director = director;

		//director.RunAsync();


		TrackedAppsViewModel = new TrackedAppsViewModel(_director);

		ToolbarViewModel = new ToolbarViewModel(_director, Navigation);


	}
}
