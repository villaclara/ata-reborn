using Application.Director.Instance;
using CommunityToolkit.Mvvm.ComponentModel;
using Serilog;
using UI.WPF.Services.Abstracts;

namespace UI.WPF.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
	public MainWindowViewModel(INavigationService navigation, IDirector director,
		ToolbarViewModel toolbarViewModel, TrackedAppsViewModel trackedAppsViewModel, TopRowViewModel topRowViewModel)
	{
		_navigation = navigation;
		_director = director;
		_director.WorkDone -= MainWindoVM_Director_WorkDone;
		_director.WorkDone += MainWindoVM_Director_WorkDone;
		LastDirectorWorkDone = DateTime.Now;

		ToolbarViewModel = toolbarViewModel;
		TrackedAppsViewModel = trackedAppsViewModel;
		TopRowViewModel = topRowViewModel;
	}

	// Update the Value displaying Last Checked date.
	private Task MainWindoVM_Director_WorkDone(object arg1, int arg2)
	{
		Log.Information("{@Method} - From ({@MainWindow}), dir LWD - {@last}.", nameof(MainWindoVM_Director_WorkDone), nameof(MainWindowViewModel), _director.LastWorkDoneDate);
		LastDirectorWorkDone = _director.LastWorkDoneDate;
		return Task.CompletedTask;
	}


	// For Binding purposes.
	public ToolbarViewModel ToolbarViewModel { get; }
	public TrackedAppsViewModel TrackedAppsViewModel { get; }
	public TopRowViewModel TopRowViewModel { get; }


	// Is taking part in Binging Current View.
	public INavigationService Navigation => _navigation;
	private readonly INavigationService _navigation;
	private readonly IDirector _director;

	[ObservableProperty]
	private DateTime _lastDirectorWorkDone;

	public string AppVersion { get; set; } = "v1.0.0.0";

}
