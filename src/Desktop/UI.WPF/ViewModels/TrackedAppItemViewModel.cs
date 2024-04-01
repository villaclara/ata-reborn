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

public partial class TrackedAppItemViewModel : ObservableObject
{
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(AppName))]
	[NotifyPropertyChangedFor(nameof(AppIsRunning))]
	[NotifyPropertyChangedFor(nameof(AppCurrentSessionHours))]
	[NotifyPropertyChangedFor(nameof(AppCurrentSessionMinutes))]
	[NotifyPropertyChangedFor(nameof(AppTotalHours))]
	[NotifyPropertyChangedFor(nameof(AppTotalMinutes))]
	[NotifyPropertyChangedFor(nameof(AppFirstSessionDate))]
	[NotifyPropertyChangedFor(nameof(AppLastSessionDate))]
	public AppInstance _app;


	public string AppName { get => App.Name; }

	public string AppIsRunning
	{
		get => App.IsRunning ? "running" : "stopped";
	}

	public uint AppCurrentSessionHours => (uint)App.CurrentSessionTime / 60;
	public uint AppCurrentSessionMinutes => (uint)App.CurrentSessionTime % 60;
	public uint AppTotalHours => (uint)App.UpTimes.Sum(u => u.Minutes) / 60;
	public uint AppTotalMinutes => (uint)App.UpTimes.Sum(u => u.Minutes) % 60;
	public string AppLastSessionDate => App.LastRunningDate.ToString("dd/MM/yy");
	public string AppFirstSessionDate => App.CreatedAt.ToString("dd/MM/yy");

	public Task Director_WorkDone(object arg1, int arg2)
	{
		var app = new DataIssuer(new ReadDataFromJsonFile()).GetAppDataByName(_app.Name);
		App = MyMapService.Map<AppInstanceVM, AppInstance>(app)!;

		

		return Task.CompletedTask;

	}

	//LIVECHARTS STUFF
	// INCLUDE IN CONSTRUCTOR OR SOMEWHERE ELSE
	//SeriesCollection = new SeriesCollection()
	//{
	//	new ColumnSeries
	//	{
	//		Title = "Time",
	//		Values = new ChartValues<double> { 10, 20, 30, 0, 20, 1, 0 }
	//	}
	//};


	//	Labels = new[] { "23/03", "24/03", "25/03", "26/03", "27/03", "28/03", "29/03" };
	//	Formatter = value => value.ToString("N");


	[ObservableProperty]
	public SeriesCollection _seriesCollection;

	[ObservableProperty]
	public string[] _labels;

	[ObservableProperty]
	public Func<double, string> _formatter;


	public TrackedAppItemViewModel(AppInstance app)
	{
		_app = app;


		_seriesCollection = new SeriesCollection()
		{
			new ColumnSeries
			{
				Title = "Time",
				Values = new ChartValues<double> {
			App.UpTimes.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-6))).FirstOrDefault()?.Minutes ?? 0,
			App.UpTimes.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-5))).FirstOrDefault()?.Minutes ?? 0,
			App.UpTimes.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-4))).FirstOrDefault()?.Minutes ?? 0,
			App.UpTimes.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-3))).FirstOrDefault()?.Minutes ?? 0,
			App.UpTimes.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-2))).FirstOrDefault()?.Minutes ?? 0,
			App.UpTimes.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1))).FirstOrDefault()?.Minutes ?? 0,
			App.UpTimes.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now)).FirstOrDefault()?.Minutes ?? 0
				}
			}
		};


		_labels = [$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-6)).ToString("dd/MM")}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-5)).ToString("dd/MM")}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-4)).ToString("dd/MM")}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-3)).ToString("dd/MM")}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-2)).ToString("dd/MM")}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1)).ToString("dd/MM")}",
			$"{DateOnly.FromDateTime(DateTime.Now).ToString("dd/MM")}"
		];
		_formatter = value => value.ToString("N");

	}
}
