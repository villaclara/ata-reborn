using Application.Common.Abstracts;
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

public partial class TrackedAppItemViewModel : BaseViewModel
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
	private AppInstanceVM _app;

	public string AppName => App.Name; 
	public string AppIsRunning => App.IsRunning ? "running" : "stopped";
	public uint AppCurrentSessionHours => (uint)App.CurrentSessionTime / 60;
	public uint AppCurrentSessionMinutes => (uint)App.CurrentSessionTime % 60;
	public uint AppTotalHours => (uint)App.UpTimeList.Sum(u => u.Minutes) / 60;
	public uint AppTotalMinutes => (uint)App.UpTimeList.Sum(u => u.Minutes) % 60;
	public string AppLastSessionDate => App.LastRunningDate.ToString("dd/MM/yy");
	public string AppFirstSessionDate => App.CreatedAt.ToString("dd/MM/yy");


	private readonly IDataIssuer _dataIssuer;


	public Task Director_WorkDone(object arg1, int arg2)
	{
		App = _dataIssuer.GetAppDataByName(App.Name) ?? App;
		//App = MyMapService.Map<AppInstanceVM, AppInstance>(app)!;

	
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


	public TrackedAppItemViewModel(AppInstanceVM app, IDataIssuer dataIssuer)
	{
		_app = app;

		_dataIssuer = dataIssuer;

		_seriesCollection = new SeriesCollection()
		{
			new ColumnSeries
			{
				Title = "Time",
				Values = new ChartValues<double> {
			App.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-6))).FirstOrDefault()?.Minutes ?? 0,
			App.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-5))).FirstOrDefault()?.Minutes ?? 0,
			App.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-4))).FirstOrDefault()?.Minutes ?? 0,
			App.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-3))).FirstOrDefault()?.Minutes ?? 0,
			App.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-2))).FirstOrDefault()?.Minutes ?? 0,
			App.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1))).FirstOrDefault()?.Minutes ?? 0,
			App.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now)).FirstOrDefault()?.Minutes ?? 0
				}
			}
		};


		_labels = [ $"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-6)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-5)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-4)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-3)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-2)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now):dd/MM}"
		];
		_formatter = value => value.ToString("N");

	}
}
