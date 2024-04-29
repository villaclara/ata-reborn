using Microsoft.Win32;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.Utilities;

public static class RegistryEditor
{
	public static int SetAppToLaunchOnStartup()
	{
		RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)!;
		reg.SetValue("ATAReborn", Process.GetCurrentProcess().MainModule!.FileName.ToString());
		Log.Information("{@Method} - Set value({@val}) in registry.", nameof(SetAppToLaunchOnStartup), "ATAReborn");
		return 0;
	}

	public static int RemoveAppFromLaunchOnStartup()
	{
		RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)!;
		reg.DeleteValue("ATAReborn");
		Log.Information("{@Method} - Del value({@val}) from registry.", nameof(RemoveAppFromLaunchOnStartup), "ATAReborn");
		return 0;
	}
}
