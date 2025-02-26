using System.Reflection;
using Application.Director.Instance;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;
using UI.WPF.Services.Abstracts;

namespace UI.WPF.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
	public MainWindowViewModel(INavigationService navigation, IDirector director, IConfigService configService, IWindowCreator wndCreator,
		ToolbarViewModel toolbarViewModel, TopRowViewModel topRowViewModel, SettingsViewModel settingsViewModel)
	{
		_navigation = navigation;
		_director = director;
		_configService = configService;
		_wndCreator = wndCreator;
		_director.WorkDone -= MainWindoVM_Director_WorkDone;
		_director.WorkDone += MainWindoVM_Director_WorkDone;
		LastDirectorWorkDone = DateTime.Now;

		ToolbarViewModel = toolbarViewModel;
		TopRowViewModel = topRowViewModel;
		SettingsViewModel = settingsViewModel;
		ThisHeight = SettingsViewModel.WindowHeight;
		ThisWidth = SettingsViewModel.WindowWidth;

		DisplayWhatsNewIfNeeded();
	}

	// Update the Value displaying Last Checked date.
	private Task MainWindoVM_Director_WorkDone(object arg1, int arg2)
	{
		Log.Information("{@Method} - From ({@MainWindow}), dir LWD - {@last}.", nameof(MainWindoVM_Director_WorkDone), nameof(MainWindowViewModel), _director.LastWorkDoneDate);
		LastDirectorWorkDone = _director.LastWorkDoneDate;
		return Task.CompletedTask;
	}

	[ObservableProperty]
	private int _thisHeight;

	[ObservableProperty]
	private int _thisWidth;

	// For Binding purposes.
	public ToolbarViewModel ToolbarViewModel { get; }
	public TopRowViewModel TopRowViewModel { get; }
	public SettingsViewModel SettingsViewModel { get; set; }


	// Is taking part in Binging Current View.
	// In MainWindow.xaml the property of Navigation.CurrentView is binded to current View.
	public INavigationService Navigation => _navigation;
	private readonly INavigationService _navigation;
	private readonly IDirector _director;
	private readonly IConfigService _configService;
	private readonly IWindowCreator _wndCreator;
	private readonly TrackedAppsViewModel_Minimal _trackedAppsViewModel_Minimal;
	[ObservableProperty]
	private DateTime _lastDirectorWorkDone;

	public string AppVersion { get; set; } = "v" + Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0";

	[RelayCommand]
	private void OpenChangelogView()
	{
		Log.Information("{@MEthod} - navigating to ChangelogView.", nameof(OpenChangelogView));
		_navigation.NavigateTo<ChangelogPageViewModel>();
	}


	partial void OnThisHeightChanged(int value)
	{
		SettingsViewModel.WindowHeight = value;
	}

	partial void OnThisWidthChanged(int value)
	{
		SettingsViewModel.WindowWidth = value;
	}

	private async void DisplayWhatsNewIfNeeded()
	{
		var isDisplayedWhatsNew = _configService.GetStringValue("WhatsNewShownVersion");
		if (isDisplayedWhatsNew == null || isDisplayedWhatsNew != AppVersion)
		{
			_configService.WriteSectionWithValue("WhatsNewShownVersion", AppVersion);
			await Task.Delay(700);
			_wndCreator.CreateWindow();
		}
	}
}
