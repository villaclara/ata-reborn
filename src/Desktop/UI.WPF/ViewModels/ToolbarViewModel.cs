using Application.Director.Instance;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;
using UI.WPF.Enums;
using UI.WPF.Services.Abstracts;

namespace UI.WPF.ViewModels;

public partial class ToolbarViewModel : BaseViewModel
{
	[ObservableProperty]
	private bool _isLightTheme = true;

	private readonly IDirector _director;

	private readonly INavigationService _navigation;

	private readonly IThemeChangeService _themeChange;

	public string ToggleMarginDefaultPos { get; set; } = "4 0 25 0";
	public string ThemeButtonIsChecked { get; set; } = "False";

	public ToolbarViewModel(IDirector director, INavigationService navigation, IThemeChangeService themeChange)
	{
		_director = director;
		_navigation = navigation;
		_themeChange = themeChange;

		var stringTheme = _themeChange.CurrentTheme switch
		{
			UIThemes.Light => "Light",
			UIThemes.Dark => "Dark",
			_ => "Light"
		};

		// Set the ToggleButton location (to the Left or to the Right) based on Theme.
		_isLightTheme = stringTheme == "Light";
		ToggleMarginDefaultPos = _isLightTheme ? "4 0 25 0" : "25 0 4 0";
		ThemeButtonIsChecked = _isLightTheme ? "False" : "True";

	}


	[RelayCommand]
	private void ShowProcessListScreen()
	{
		Log.Information("{@Method} - Navigating to {@view}", nameof(ShowProcessListScreen), typeof(ProcessListViewModel));
		_navigation.NavigateTo<ProcessListViewModel>();
	}


	[RelayCommand]
	private void ShowHomeScreen()
	{
		Log.Information("{@Method} - Navigating to {@view}", nameof(ShowHomeScreen), typeof(TrackedAppsViewModel));
		_navigation.NavigateTo<TrackedAppsViewModel>();
	}

	[RelayCommand]
	private async Task RunDirectorOnce()
	{
		await _director.RunOnceManuallyAsync();
	}

	[RelayCommand]
	private void ShowSettingsScreen()
	{
		Log.Information("{@Method} - Navigating to {@view}", nameof(ShowProcessListScreen), typeof(SettingsViewModel));
		_navigation.NavigateTo<SettingsViewModel>();
	}

	[RelayCommand]
	private void ToggleDayNightTheme()
	{
		IsLightTheme = !IsLightTheme;

		// set the value in UI.WPF.dll.Config
		var newTheme = IsLightTheme switch
		{
			false => UIThemes.Dark,
			_ => UIThemes.Light
		};
		Log.Information("{@Method} - New theme - {@theme}.", nameof(ToggleDayNightTheme), newTheme);
		_themeChange.SetTheme(newTheme);

	}




}
