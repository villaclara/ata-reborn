using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Abstracts;

/// <summary>
/// Gets the processes as <see cref="IDictionary{TKey, TValue}"/>.
/// </summary>
public interface IGetProcs
{
	/// <summary>
	/// Gets the Unique Processes Dictionary. <br/>
	/// Depending what value is passed as argument the Dictionary will be: <br/>
	/// True - TKey - processName, TValue - appName. <br/>
	/// False - TKey - processName, TValue - processName.
	/// </summary>
	/// <param name="useSystemManagement">Pass True if want to try SystemManagement method. Otherwise pass False.</param>
	/// <returns></returns>
	IDictionary<string, string> GetUniqueProcesses(bool useSystemManagement);
	IEnumerable<UniqueProcess> GetUniqueProcessesAsList(bool useSystemManagement);
}
