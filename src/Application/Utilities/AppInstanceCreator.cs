using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilities;


public class AppInstanceCreator
{
	/// <summary>
	/// Create and return new object of <see cref="AppInstance"/>.
	/// </summary>
	/// <param name="appName">Name of Application, some fancy one.</param>
	/// <param name="processName">Name of process in the OS. It is mandatory.</param>
	/// <returns>New object of <see cref="AppInstance"/>.</returns>
	public static AppInstance CreateAppInstanceToTrack(string? appName, string processName) =>
		new AppInstance()
		{
			Name = appName ?? processName,
			ProcessNameInOS = processName,
			CreatedAt = DateTime.Now,
		};

}
