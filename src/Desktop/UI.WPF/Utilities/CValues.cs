using System.IO;

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
	public static string SettingsFile
	{
		get => _settingsFile;
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
	}
}
