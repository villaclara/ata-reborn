using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilities;

public static class ConstantValues
{

	// path to AppData/Roaming/
	private static readonly string _appDataRoamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

	// Full path to AppData/Roaming/ATA Reborn. 
	// Is used when creating MAIN_FILE and LOG_FILE.
	public static string DIRECTORY_FILE_NAME { get; }

	private static string _main_filename = "apps.json";
	public static string MAIN_FILE_NAME 
	{ 
		get => _main_filename; 
		set => _main_filename = Path.Combine(DIRECTORY_FILE_NAME, value); 
	}


	private static string _backup_main_filename = "apps_backup.json";
	public static string BACKUP_MAIN_FILE_NAME
	{
		get => _backup_main_filename;
		set => _backup_main_filename = Path.Combine(DIRECTORY_FILE_NAME, value);
	}

	private static readonly string _log_filename;
	public static string LOG_FILE_NAME
	{
		get => _log_filename;
	}


	// THIS TWO SHOULD BE CHANGED TOGETHER. M - Minutes, MS - Miliseconds. 
	public static int TIMER_INTERVAL_M = 1;
	public static int TIMER_INTERVAL_MS = 15000;

	static ConstantValues()
	{
		// create if needed directory AppData/Roaming/ATA Reborn
		var dirPath = Path.Combine(_appDataRoamingPath, "ATA Reborn");
		if(!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
		}

		DIRECTORY_FILE_NAME = dirPath;

		_log_filename = Path.Combine(DIRECTORY_FILE_NAME, "log.txt");
	}
}
