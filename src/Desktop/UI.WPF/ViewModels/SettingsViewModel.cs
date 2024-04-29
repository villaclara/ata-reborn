using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using CommunityToolkit.Mvvm.ComponentModel;
using UI.WPF.Utilities;
using UI.WPF.Services.Abstracts;
using System.Windows.Automation;
using System.Windows;
using Microsoft.Win32;
using System.Diagnostics;

namespace UI.WPF.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
	private readonly IConfigService _configService;


	private int _windowHeight;
	public int WindowHeight 
	{
		get => _windowHeight;
		set
		{
			_windowHeight = value;
			_configService.WriteSectionWithValue("WindowHeight", WindowHeight.ToString());
			Log.Information("{@Method} - Set Window Height ({@h}).", nameof(WindowHeight), value);
		}
	}

	private int _windowWidth;
	public int WindowWidth 
	{ 
		get => _windowWidth;
		set
		{
			_windowWidth = value;
			_configService.WriteSectionWithValue("WindowWidth", WindowWidth.ToString());
			Log.Information("{@Method} - Set Window Width ({@h}).", nameof(WindowWidth), value);
		}
	}

	private readonly int _heightAtStart = 0;
	private readonly int _widthAtStart = 0;

	[ObservableProperty]
	private bool _isLaunchOnStartup = false;
	private bool _launchAtStart = false;


	[ObservableProperty]
	private string _saveResultText = "";

	[ObservableProperty]
	public string _visibilityResultText = "Hidden";

	public SettingsViewModel(IConfigService writeConfigService)
	{
		_configService = writeConfigService;

		var h = _configService.GetIntValue("WindowHeight");
		Log.Information("{@Method} - Height value from config ({@h}).", nameof(SettingsViewModel), h);	
		WindowHeight = h != 0 
			? h 
			: CValues.DEFAULT_WINDOW_HEIGHT;
		_heightAtStart = WindowHeight;

		var w = _configService.GetIntValue("WindowWidth");
		Log.Information("{@Method} - Width value from config ({@h}).", nameof(SettingsViewModel), w);
		WindowWidth = w != 0 
			? w 
			: CValues.DEFAULT_WINDOW_WIDTH;
		_widthAtStart = WindowWidth;

		Log.Information("{@Method} - Set Height ({@h}), Width ({@w}).", nameof(SettingsViewModel), WindowHeight, WindowWidth);

		// Get the launch on startup option 
		// Get string from config file
		// If the string is missing - we assume that it is first launch of the App and
		if(_configService.GetStringValue("LaunchOnStartup") is null)
		{
			IsLaunchOnStartup = true;
			Log.Information("{@Method} - LaunchOnStartup ({@launch}).", nameof(SettingsViewModel), IsLaunchOnStartup);
			_launchAtStart = IsLaunchOnStartup;

			// write info into Registry
			_configService.WriteSectionWithValue("LaunchOnStartup", "True");
		}
		else		// It is at least second launch
		{
			IsLaunchOnStartup = _configService.GetBooleanValue("LaunchOnStartup");
			Log.Information("{@Method} - LaunchOnStartup ({@launch}).", nameof(SettingsViewModel), IsLaunchOnStartup);
			_launchAtStart = IsLaunchOnStartup;
		}

	}

	[RelayCommand]
	private async Task SaveChanges()
	{
		// When there was at least one error in changes we set the result to false and skip all following changes.
		bool result = true;

		// Check if the value differs from the current. If it is the same we do not need to perform any Save action.
		if(_launchAtStart != IsLaunchOnStartup && result)
		{
			//result = _configService.WriteSectionWithValue("LaunchOnStartup", IsLaunchOnStartup ? "True" : "False");
			//var resStartup = IsLaunchOnStartup ?
			//	RegistryEditor.SetAppToLaunchOnStartup() :
			//	RegistryEditor.RemoveAppFromLaunchOnStartup();
			//Log.Information("{@Method} - Changed and saved new launchonstartup option - {@opt}.", nameof(SaveChanges), IsLaunchOnStartup ? "True" : "False");


			var startupTask = await Windows.ApplicationModel.StartupTask.GetAsync("ATARebornTask");
			
			// disabled before, user checked checkbox
			if(IsLaunchOnStartup && startupTask.State == Windows.ApplicationModel.StartupTaskState.Disabled)
			{
				await startupTask.RequestEnableAsync();
				Log.Information("{@MEthod} - Request to enable autorun.", nameof(SaveChanges));
			}

			// enabled before, user unchecked checkbox
			if(!IsLaunchOnStartup && startupTask.State == Windows.ApplicationModel.StartupTaskState.Enabled)
			{
				startupTask.Disable();
				Log.Information("{@Method} - Disable autorun.", nameof(SaveChanges));
			}

			result = _configService.WriteSectionWithValue("LaunchOnStartup", IsLaunchOnStartup ? "True" : "False");
			Log.Information("{@Method} - Changed and saved new launchonstartup option - {@opt}.", nameof(SaveChanges), IsLaunchOnStartup ? "True" : "False");

			_launchAtStart = IsLaunchOnStartup;
		}

		SaveResultText = result ? "Changes Saved." : "Error when saving changes. Please try again.";
		VisibilityResultText = "Visible";
		await Task.Delay(2000);
		VisibilityResultText = "Hidden";

	}

	[RelayCommand]
	private void RestoreDefaultSize()
	{
		WindowHeight = CValues.DEFAULT_WINDOW_HEIGHT;
		WindowWidth = CValues.DEFAULT_WINDOW_WIDTH;


		// Violates the MVVM, but I do not know how to do it in other way. Sorry
		System.Windows.Application.Current.MainWindow.Height = WindowHeight;
		System.Windows.Application.Current.MainWindow.Width = WindowWidth;
		
		// Check if the values were changed from when starting app.
		if (_heightAtStart != WindowHeight)
		{
			_configService.WriteSectionWithValue("WindowHeight", WindowHeight.ToString());
		}

		if (_widthAtStart != WindowWidth)
		{
			_configService.WriteSectionWithValue("WindowWidth", WindowWidth.ToString());
		}

		Log.Information("{@Method} - Set default values for Height ({@h}), Width ({@w}).", nameof(SettingsViewModel), WindowHeight, WindowWidth);

	}

}
