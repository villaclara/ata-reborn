﻿using System.IO;

namespace UI.WPF.Utilities;

/// <summary>
/// Constant Values for WPF proj
/// </summary>
public static class CValues
{
	public const int DEFAULT_WINDOW_HEIGHT = 530;
	public const int DEFAULT_WINDOW_WIDTH = 870;

	private static readonly string _appDataRoamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

	private static readonly string _settingsFile;
	private static readonly string _changelogFile;

	public static string SettingsFile
	{
		get => _settingsFile;
	}

	public static string ChangelogFile
	{
		get => _changelogFile;
	}

	static CValues()
	{
		// create if needed directory AppData/Roaming/ATA Reborn
		var dirPath = Path.Combine(_appDataRoamingPath, "ATA Reborn");
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
		}

		_settingsFile = Path.Combine(dirPath, "UIConfig.json");
		//_changelogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Changelog.json");
		_changelogFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!, "Changelog.json");
	}


	#region Settings Names 

	public const string INT_WINDOW_HEIGHT = "WindowHeight";
	public const string INT_WINDOW_WIDTH = "WindowWidth";
	public const string BOOL_LAUNCH_AT_STARTUP = "LaunchOnStartup";
	public const string BOOL_START_MINIMIZED = "StartMinimized";
	public const string STR_THEME = "Theme";
	public const string STR_WHATS_NEW_SHOWN_VERSION = "WhatsNewShownVersion";
	public const string BOOL_MINIMAL_DASHBOARD = "MinimalDashboard";

	#endregion
}
