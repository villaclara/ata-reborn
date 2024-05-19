using Application.Common.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Serilog;
using Shared.ViewModels;
using UI.WPF.Enums;
using UI.WPF.Services;
using UI.WPF.Services.Abstracts;

namespace UI.WPF.ViewModels;

public partial class TrackedAppItemViewModel : BaseViewModel
{
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(AppName))]
	[NotifyPropertyChangedFor(nameof(AppIsRunning))]
	[NotifyPropertyChangedFor(nameof(AppCurrentSessionHours))]
	[NotifyPropertyChangedFor(nameof(AppCurrentSessionMinutes))]
	[NotifyPropertyChangedFor(nameof(AppTodayTimeHours))]
	[NotifyPropertyChangedFor(nameof(AppTodayTimeMinutes))]
	[NotifyPropertyChangedFor(nameof(AppTotalHours))]
	[NotifyPropertyChangedFor(nameof(AppTotalMinutes))]
	[NotifyPropertyChangedFor(nameof(AppFirstSessionDate))]
	[NotifyPropertyChangedFor(nameof(AppLastSessionDate))]
	[NotifyPropertyChangedFor(nameof(SeriesCollection))]
	private AppInstanceVM _app;

	public string AppName => App.Name;
	public string AppIsRunning => App.IsRunning ? "running" : "stopped";
	public uint AppCurrentSessionHours => (uint)App.CurrentSessionTime / 60;
	public uint AppCurrentSessionMinutes => (uint)App.CurrentSessionTime % 60;
	public uint AppTodayTimeHours => (uint)App.UpTimeList.Find(u => u.Date == DateOnly.FromDateTime(DateTime.Now))!.Minutes  / 60;
	public uint AppTodayTimeMinutes => (uint)App.UpTimeList.Find(u => u.Date == DateOnly.FromDateTime(DateTime.Now))!.Minutes  % 60;
	public uint AppTotalHours => (uint)App.UpTimeList.Sum(u => u.Minutes) / 60;
	public uint AppTotalMinutes => (uint)App.UpTimeList.Sum(u => u.Minutes) % 60;
	public string AppLastSessionDate => App.LastRunningDate.ToString("dd/MM/yy");
	public string AppFirstSessionDate => App.CreatedAt.ToString("dd/MM/yy");


	private readonly IDataIssuer _dataIssuer;
	private readonly ICustomDialogService _customDialog;
	private readonly IRetrieveChartService _retrieveChartService;


	// Reassign tracked App to new values.
	public Task TrackedAppItemVM_Director_WorkDone(object arg1, int arg2)
	{
		Log.Information("{@Method} - Get data for ({@app}).", nameof(TrackedAppItemVM_Director_WorkDone), App.Name);
		App = _dataIssuer.GetAppDataByName(App.Name) ?? App;


		SeriesCollection.Last().Values.Clear();
		SeriesCollection.Last().Values.AddRange(new ChartValues<ObservableValue> {
					new ObservableValue() { Value =
						 App.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-6))).FirstOrDefault()?.Minutes ?? 0,
					},
					new ObservableValue() { Value =
						App.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-5))).FirstOrDefault()?.Minutes ?? 0,
					},
					new ObservableValue() { Value =
						App.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-4))).FirstOrDefault()?.Minutes ?? 0,
					},
					new ObservableValue() { Value =
						App.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-3))).FirstOrDefault()?.Minutes ?? 0,
					},
					new ObservableValue() { Value =
						App.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-2))).FirstOrDefault()?.Minutes ?? 0,
					},
					new ObservableValue() { Value =
						App.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1))).FirstOrDefault()?.Minutes ?? 0,
					},
					new ObservableValue() { Value =
						App.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now)).FirstOrDefault()?.Minutes ?? 0
					}
				});

		//var i = 0;
		//foreach(var s in SeriesCollection.Last().Values)
		//{
		//	if(i < SeriesCollection.Last().Values.Count - 1)
		//	{
		//		i++;
		//		continue;
		//	}

		//	s = new ObservableValue()
		//	{
		//		Value =
		//				App.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now)).FirstOrDefault()?.Minutes ?? 0
		//	};

		//}

		Log.Information("{@Method} - ({@App}) values updated.", nameof(TrackedAppItemVM_Director_WorkDone), App.Name);
		return Task.CompletedTask;

	}

	[ObservableProperty]
	public SeriesCollection _seriesCollection;

	[ObservableProperty]
	public string[] _labels;

	[ObservableProperty]
	public Func<double, string> _formatter;


	public TrackedAppItemViewModel(AppInstanceVM app,
		IDataIssuer dataIssuer, ICustomDialogService customDialog, IRetrieveChartService retrieveChartService)
	{
		_app = app;

		_dataIssuer = dataIssuer;
		_customDialog = customDialog;
		_retrieveChartService = retrieveChartService;

		//_seriesCollection = _retrieveChartService.GetSeriesForAppLastWeek(_app);
		_seriesCollection = new SeriesCollection
		{
			new ColumnSeries
			{
				Title = "Time",
				Values = new ChartValues<ObservableValue> {
					new ObservableValue() { Value =
						 app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-6))).FirstOrDefault()?.Minutes ?? 0,
					},
					new ObservableValue() { Value =
						app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-5))).FirstOrDefault()?.Minutes ?? 0,
					},
					new ObservableValue() { Value =
						app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-4))).FirstOrDefault()?.Minutes ?? 0,
					},
					new ObservableValue() { Value =
						app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-3))).FirstOrDefault()?.Minutes ?? 0,
					},
					new ObservableValue() { Value =
						app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-2))).FirstOrDefault()?.Minutes ?? 0,
					},
					new ObservableValue() { Value =
						app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1))).FirstOrDefault()?.Minutes ?? 0,
					},
					new ObservableValue() { Value =
						app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now)).FirstOrDefault()?.Minutes ?? 0
					}
				},

			}
		};
		_labels = _retrieveChartService.GetLabelsLastWeek();
		_formatter = value => value.ToString("N");
	}



	[RelayCommand]
	private void DeleteTrackedApp()
	{
		try
		{
			// Maybe not so good to put it here, but anyway it works.
			var result = _customDialog.ShowYesNoDialog("WTF", "You sure want to remove application form tracking?");
			if (result == CustomDialogResult.Yes)
			{
				StrongReferenceMessenger.Default.Send(new TrackedAppDeletedMessage(AppName));

			}
		}
		catch (Exception ex)
		{
			Log.Error("{@Method} - Exception - {@ex}", nameof(DeleteTrackedApp), ex.Message);
		}
	}

	[RelayCommand]
	private void ShowFullHistoryView()
	{
		Log.Information("{@Method} - Show full history for ({@app}) button clicked.", nameof(ShowFullHistoryView), App.Name);
		StrongReferenceMessenger.Default.Send<FullHistoryForAppTriggeredMessage>(new FullHistoryForAppTriggeredMessage(this.App));
	}
}
