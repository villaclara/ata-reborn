using Application.Common.Services;
using Application.Director.Creation;
using Application.Models;
using Application.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using Serilog;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.Components;

namespace UI.WPF.ViewModels;

public  class MainWindowViewModel : ObservableObject
{
	//public ObservableCollection<TrackedAppItemViewModel> AppItems { get; }
	public List<TrackedAppItemViewModel> AppItems { get; }



	public ToolbarViewModel ToolbarViewModel { get; }

	public ProcessListViewModel ProcessListViewModel { get; }

	public TrackedAppsViewModel TrackedAppsViewModel { get; }

	public MainWindowViewModel()
	{
		//AppItems = new ObservableCollection<TrackedAppItemViewModel>();
		AppItems = [];

		File.WriteAllText("log.txt", "");

		Log.Logger = new LoggerConfiguration()
			.WriteTo.Console()
			.WriteTo.File("log.txt")
			.CreateLogger();


		Log.Information("{@Method} - start.", nameof(MainWindowViewModel));

		var director = new DirectorBuilder()
			.AddIOServices(new ReadDataFromJsonFile(), new WriteDataStringToFile())
			.SetWritableFile("apps.json")
			.SetTimerCheckValue(5000)
			.Build();

		director.RunAsync();

		//var dataIssuer = new DataIssuer(new ReadDataFromJsonFile());

		//foreach (var app in director.Apps)
		//{
		//	var appVM = MyMapService.Map<AppInstance, AppInstanceVM>(app);

		//	if (appVM != null)
		//	{
		//		TrackedAppItemViewModel vm = new TrackedAppItemViewModel(appVM, dataIssuer);
		//		director.WorkDone += vm.Director_WorkDone;
		//		AppItems.Add(vm);
		//	}


		//	//TrackedAppItem item = new TrackedAppItem();
		//	//item.DataContext = vm;
		//	//item.Width = 300;

		//}

		TrackedAppsViewModel = new TrackedAppsViewModel(director);

		ToolbarViewModel = new ToolbarViewModel(director);

		ProcessListViewModel = new ProcessListViewModel(new GetProcsService());

	}
}
