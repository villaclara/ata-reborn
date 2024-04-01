using Application.Common.Services;
using Application.Models;
using Application.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveCharts;
using LiveCharts.Wpf;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.ViewModels;

public partial class TrackedAppItemViewModel(AppInstance app) : ObservableObject
{
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(AppName))]
	[NotifyPropertyChangedFor(nameof(AppIsRunning))]
	[NotifyPropertyChangedFor(nameof(AppCurrentSessionMinutes))]
	public AppInstance _app = app;


	public string AppName { get => App.Name; }

	public string AppIsRunning
	{
		get => App.IsRunning ? "running" : "stopped";
	}

	public double AppCurrentSessionMinutes => Math.Round(App.CurrentSessionTime, 2);

	public Task Director_WorkDone(object arg1, int arg2)
	{
		var app = new DataIssuer(new ReadDataFromJsonFile()).GetAppDataByName(_app.Name);
		App = MyMapService.Map<AppInstanceVM, AppInstance>(app)!;
		
		return Task.CompletedTask;
	}

	// LIVECHARTS STUFF
	// INCLUDE IN CONSTRUCTOR OR SOMEWHERE ELSE
	//	SeriesCollection = new SeriesCollection()
	//	{
	//		new ColumnSeries
	//		{
	//			Title = "Time",
	//			Values = new ChartValues<double> { 10, 20, 30, 0, 20, 1, 0 }
	//		}
	//		};


	//	Labels = new[] { "23/03", "24/03", "25/03", "26/03", "27/03", "28/03", "29/03" };
	//Formatter = value => value.ToString("N");

	public SeriesCollection SeriesCollection { get; set; }
	public string[] Labels { get; set; }
	public Func<double, string> Formatter { get; set; }
}
