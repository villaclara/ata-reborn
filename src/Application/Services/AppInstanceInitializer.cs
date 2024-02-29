using Application.Abstracts;
using Application.AppToTrack.Interactors;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

/// <summary>
/// Class used to create and return new <see cref="IInteractor"/> object with initialized <see cref="AppInstance"/> inside.
/// <br/> Has both static and common methods.
/// </summary>
public class AppInstanceInitializer : IAppInstanceInitialize
{

	// to do
	//
	// Add inside method to accept real values of the AppInstance objects to create.
	public IInteractor InitializeAppInstanceToTrack()
	{
		var app = new AppInstance()
		{
			ProcessNameInOS = "ATA_WPF",
			Name = "ATA_WPF",
			IsRunning = false,
			TimeRunning = 0
		};

		return new Interactor(app);
	}

	public static IInteractor InitializeNewAppToTrack()
	{
		var app = new AppInstance()
		{
			ProcessNameInOS = "Discord",
			Name = "Discord",
			IsRunning = false,
			TimeRunning = 0
		};

		return new Interactor(app);
	}
}
