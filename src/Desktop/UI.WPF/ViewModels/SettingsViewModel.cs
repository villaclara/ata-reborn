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

namespace UI.WPF.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
	private readonly IConfigService _configService;
	

	public int WindowHeight { get; set; } = 0;
	public int WindowWidth { get; set; } = 0;

	private readonly int _heightAtStart = 0;
	private readonly int _widthAtStart = 0;

	[ObservableProperty]
	private bool _isLaunchOnStartup = false;
	private readonly bool _launchAtStart = false;


	[ObservableProperty]
	private string _saveResultText = "";

	[ObservableProperty]
	public string _visibilityResultText = "Hidden";

	public SettingsViewModel(IConfigService writeConfigService)
	{
		_configService = writeConfigService;

		var h = _configService.GetIntValue("WindowHeight");
		Log.Information("{@Method} - Height value from config ({@h}).", nameof(SettingsViewModel), h);	
		WindowHeight = h != 0 ? h : CValues.DEFAULT_WINDOW_HEIGHT;
		_heightAtStart = WindowHeight;

		var w = _configService.GetIntValue("WindowWidth");
		Log.Information("{@Method} - Width value from config ({@h}).", nameof(SettingsViewModel), w);
		WindowWidth = w != 0 ? w : CValues.DEFAULT_WINDOW_WIDTH;
		_widthAtStart = WindowWidth;

		Log.Information("{@Method} - Set Height ({@h}), Width ({@w}).", nameof(SettingsViewModel), WindowHeight, WindowWidth);

		IsLaunchOnStartup = _configService.GetBooleanValue("LaunchOnStartup");
		Log.Information("{@Method} - LaunchOnStartup ({@launch}).", nameof(SettingsViewModel), IsLaunchOnStartup);
		_launchAtStart = IsLaunchOnStartup;
	}

	[RelayCommand]
	private async Task SaveChanges()
	{
		// When there was at least one error in changes we set the result to false and skip all following changes.
		bool result = true;

		// Check if the values were changed from when starting app.
		if (_heightAtStart != WindowHeight && result)
		{
			result = _configService.WriteSectionWithValues("WindowHeight", WindowHeight.ToString());
		}

		if(_widthAtStart != WindowWidth && result)
		{
			result = _configService.WriteSectionWithValues("WindowWidth", WindowWidth.ToString());
		}

		if(_launchAtStart != IsLaunchOnStartup && result)
		{
			result = _configService.WriteSectionWithValues("LaunchOnStartup", IsLaunchOnStartup ? "True" : "False");
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
			_configService.WriteSectionWithValues("WindowHeight", WindowHeight.ToString());
		}

		if (_widthAtStart != WindowWidth)
		{
			_configService.WriteSectionWithValues("WindowWidth", WindowWidth.ToString());
		}
	}
}
