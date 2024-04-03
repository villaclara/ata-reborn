using Application.Common.Services;
using Application.Director.Creation;
using CommunityToolkit.Mvvm.ComponentModel;
using Serilog;
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
	public ObservableCollection<TrackedAppItemViewModel> AppItems { get; }

	public MainWindowViewModel()
	{
		AppItems = new ObservableCollection<TrackedAppItemViewModel>();

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

		foreach (var app in director.Apps)
		{
			TrackedAppItemViewModel vm = new TrackedAppItemViewModel(app);
			director.WorkDone += vm.Director_WorkDone;
			//TrackedAppItem item = new TrackedAppItem();
			//item.DataContext = vm;
			//item.Width = 300;

			AppItems.Add(vm);
		}
	}
}
