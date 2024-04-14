using Application.Director.Instance;
using CommunityToolkit.Mvvm.Input;
using Serilog;
using System.Configuration;
using System.IO;
using System.Windows;
using UI.WPF.Services;

namespace UI.WPF.ViewModels;

public partial class ToolbarViewModel : BaseViewModel
{
	private bool _isLightTheme = true;

	private readonly IDirector _director;

	private readonly INavigationService _navigation;

	public string ToggleMarginDefaultPos { get; set; } = "4 0 25 0";
	public string ThemeButtonIsChecked { get; set; } = "False";

	public ToolbarViewModel(IDirector director, INavigationService navigation)
	{
		_director = director;
		_navigation = navigation;

		var isLight = System.Configuration.ConfigurationManager.AppSettings["IsLightTheme"];

		// Set theme at launch. Should move to Init method.
		_isLightTheme = isLight == "True";
		ToggleMarginDefaultPos = _isLightTheme ? "4 0 25 0" : "25 0 4 0";
		ThemeButtonIsChecked = _isLightTheme ? "False" : "True";

		string newThemePath = _isLightTheme ? "Resources/Dictionaries/LightTheme.xaml" : "Resources/Dictionaries/DarkTheme.xaml";
		var newTheme = (ResourceDictionary)System.Windows.Application.LoadComponent(new Uri(newThemePath, UriKind.Relative));
		System.Windows.Application.Current.Resources.MergedDictionaries.Clear();
		System.Windows.Application.Current.Resources.MergedDictionaries.Add(newTheme);
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
	private void ShowSettingsScreen()
	{

	}

	[RelayCommand]
	private void ToggleDayNightTheme()
	{
		_isLightTheme = !_isLightTheme;

		string s = _isLightTheme ? "True" : "False";
		// set the value in App.Config



		//System.Configuration.ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
		//fileMap.ExeConfigFilename = Directory.GetCurrentDirectory() + "\\App.config";
		//Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

		//if(config.AppSettings.Settings["IsLightTheme"] is null)
		//{
		//	config.AppSettings.Settings.Add("IsLightTheme", s);
		//}
		//else
		//{
		//	config.AppSettings.Settings["IsLightTheme"].Value = _isLightTheme ? "True" : "False";
		//}
		//config.Save(ConfigurationSaveMode.Modified);

		var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
		config.AppSettings.Settings["IsLightTheme"].Value = _isLightTheme ? "True" : "False";
		config.Save(ConfigurationSaveMode.Modified);

		string newThemePath = _isLightTheme ? "Resources/Dictionaries/LightTheme.xaml" : "Resources/Dictionaries/DarkTheme.xaml";
		var newTheme = (ResourceDictionary)System.Windows.Application.LoadComponent(new Uri(newThemePath, UriKind.Relative));
		System.Windows.Application.Current.Resources.MergedDictionaries.Clear();
		System.Windows.Application.Current.Resources.MergedDictionaries.Add(newTheme);
	}




}
