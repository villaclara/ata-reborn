using Application.Common.Abstracts;
using Shared.Models;
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

	/// <summary>
	/// Gets the Unique Processes Dictionary.
	/// </summary>
	/// <param name="useSystemManagement">Pass any true/false to trigger this overloaded method.</param>
	/// <returns><see cref="IDictionary{TKey, TValue}"/> object with TKey - processName, TValue - appName.</returns>
	public IDictionary<string, string> GetUniqueProcesses(bool useSystemManagement)
	{
		var result = new Dictionary<string, string>();

		var procs = Process.GetProcesses().Distinct();

		foreach (var proc in procs)
		{
			if(useSystemManagement)
			{
				var extraInfo = GetExtraInfo(proc.Id);
				
				if (!result.ContainsKey(proc.ProcessName))
				{
					result.Add(proc.ProcessName, extraInfo.Item2 ?? proc.ProcessName);
				}
			}
			else
			{
				result.Add(proc.ProcessName, proc.ProcessName);
			}
		}

		return result;
	}


	public IEnumerable<UniqueProcess> GetUniqueProcessesAsList(bool useSystemManagement)
	{
		var result = new Dictionary<string, string>();
		var resultList = new List<UniqueProcess>();

		var procs = Process.GetProcesses().Distinct();

		foreach (var proc in procs)
		{
			if (useSystemManagement)
			{
				var extraInfo = GetExtraInfo(proc.Id);

				if (!result.ContainsKey(proc.ProcessName))
				{
					result.Add(proc.ProcessName, extraInfo.Item2 ?? proc.ProcessName);
					resultList.Add(new UniqueProcess
					{
						ProcessName = proc.ProcessName,
						AppName = extraInfo.Item2 ?? proc.ProcessName
					});
				}
			}
			else
			{
				if (!result.ContainsKey(proc.ProcessName))
				{
					result.Add(proc.ProcessName, proc.ProcessName);
					resultList.Add(new UniqueProcess
					{
						ProcessName = proc.ProcessName,
						AppName = proc.ProcessName
					});
				}
			}
		}

		return resultList;
	}

	// first string - appName, second string - processName
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
					// username - not needed if we dont care whou is user
					strs.Item1 = args[0];
				}


				if (obj["ExecutablePath"] != null)
				{
					// short process name as in Task Manager
					// f.e. Microsoft Visual Studio 2022 instead of devenv.exe
					strs.Item2 = FileVersionInfo.GetVersionInfo(obj["ExecutablePath"].ToString()!).FileDescription!;
				}
			}

		}
		return strs;
	}
}
