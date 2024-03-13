using Application.Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Services;

public class GetProcsService : IGetProcs
{
	public IDictionary<string, string> GetUniqueProcesses()
	{
		var result = new Dictionary<string, string>();

		var procs = Process.GetProcesses().Distinct();

		foreach(var proc in procs)
		{
			result.Add(proc.ProcessName, proc.ProcessName);
		}

		return result;

		
	}

	public IDictionary<string, string> GetUniqueProcesses(bool anyvalue)
	{
		var result = new Dictionary<string, string>();

		var procs = Process.GetProcesses().Distinct();

		foreach (var proc in procs)
		{
			var extraInfo = GetExtraInfo(proc.Id);
			result.Add(proc.ProcessName, extraInfo.Item2 ?? proc.ProcessName);
		}

		return result;

	}

	private static (string?, string?) GetExtraInfo(int pId)
	{
		(string?, string?) strs = (null, null);

		// This check is not mandatory
		// sbut otherwise few warnings will be shown regarding 'This call site is reachable on all platforms' but InvokeMethod f.e. is only Windows 
		if(OperatingSystem.IsWindows())
		{
			// using System.Management
			// it is pretty slow so idk
			string query = "Select * From Win32_Process Where ProcessID = " + pId;
			ManagementObjectSearcher searcher = new(query);
			ManagementObjectCollection collect = searcher.Get();

			foreach (ManagementObject obj in collect)
			{
				string[] args = [string.Empty, string.Empty];
				int returncode = Convert.ToInt32(obj.InvokeMethod("GetOwner", args));
				if (returncode == 0)
				{
					// username
					strs.Item1 = args[0];
				}


				if (obj["ExecutablePath"] != null)
				{
					// process name as in Task Manager
					strs.Item2 = FileVersionInfo.GetVersionInfo(obj["ExecutablePath"].ToString()!).FileDescription!;
				}
			}

		}
		return strs;
	}
}
