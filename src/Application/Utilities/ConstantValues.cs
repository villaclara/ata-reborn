using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilities;

public static class ConstantValues
{
	public const string MAIN_FILE_NAME = "apps.json";
	public const string BACKUP_MAIN_FILE_NAME = "apps_backup.json";


	// THIS TWO SHOULD BE CHANGED TOGETHER. M - Minutes, MS - Miliseconds. 
	public const int TIMER_INTERVAL_M = 1;
	public const int TIMER_INTERVAL_MS = 5000;
}
