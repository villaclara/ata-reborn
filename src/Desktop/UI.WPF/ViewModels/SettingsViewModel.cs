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

namespace UI.WPF.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
	public int WindowHeight { get; set; } = 0;
	public int WindowWidth { get; set; } = 0;


	[ObservableProperty]
	private string _saveResultText = "";

	[ObservableProperty]
	private bool _isLaunchOnStartup = false;

	private IConfigService _writeConfig;


	public SettingsViewModel(IConfigService writeConfigService)
	{
		_writeConfig = writeConfigService;

		var h = _writeConfig.GetValueFromSection("WindowHeight");
		try
		{
			var convertedHeightFromConfig = Convert.ToInt32(h);
			if(convertedHeightFromConfig == 0)
			{
				throw new ArgumentNullException(nameof(convertedHeightFromConfig));
			}
			WindowHeight = convertedHeightFromConfig;
		}
		catch(Exception ex)
		{
			Log.Error("{@Method} - error ({@err}).", nameof(SettingsViewModel), ex.Message);
			WindowHeight = CValues.DEFAULT_WINDOW_HEIGHT;
		}


		var w = ConfigurationManager.AppSettings["WindowWidth"];
		try
		{
			var convertedWidthFromConfig = Convert.ToInt32(w);
			if(convertedWidthFromConfig == 0)
			{
				throw new ArgumentNullException(nameof(convertedWidthFromConfig));
			}
			WindowWidth = convertedWidthFromConfig;
		}
		catch (Exception ex)
		{
			Log.Error("{@Method} - error ({@err}).", nameof(SettingsViewModel), ex.Message);
			WindowWidth = CValues.DEFAULT_WINDOW_WIDTH;
		}

		Log.Information("{@Method} - Set Height ({@h}), Width ({@w}).", nameof(SettingsViewModel), WindowHeight, WindowWidth);

		var launch = ConfigurationManager.AppSettings["LaunchOnStartup"];
		IsLaunchOnStartup = launch switch
		{
			"True" => true,
			_ => false,
		};
		Log.Information("{@Method} - LaunchOnStartup ({@launch}).", nameof(SettingsViewModel), IsLaunchOnStartup);

	}

	[RelayCommand]
	private void SaveChanges()
	{
		
		_writeConfig.WriteSectionWithValues("WindowHeight", WindowHeight.ToString());
		_writeConfig.WriteSectionWithValues("WindowWidth", WindowWidth.ToString());
		_writeConfig.WriteSectionWithValues("LaunchOnStartup", IsLaunchOnStartup ? "True" : "False");

		SaveResultText = "Changes Saved.";
	}
}
