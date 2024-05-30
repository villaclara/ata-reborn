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
	public uint AppTotalHours => (uint)App.UpTimeList.Sum(u => u.Minutes) / 60;
	public uint AppTotalMinutes => (uint)App.UpTimeList.Sum(u => u.Minutes) % 60;
	public string AppLastSessionDate => App.LastRunningDate.ToString("dd/MM/yy");
	public string AppFirstSessionDate => App.CreatedAt.ToString("dd/MM/yy");

	// Here we should perform check for nullability. Because if the App were not running Today the null will be returned from LINQ. And then we need to set the value to 0.
	public uint AppTodayTimeHours => App.UpTimeList.FirstOrDefault(u => u.Date == DateOnly.FromDateTime(DateTime.Now)) != null
		? (uint)App.UpTimeList.FirstOrDefault(u => u.Date == DateOnly.FromDateTime(DateTime.Now))!.Minutes / 60
		: 0;
	public uint AppTodayTimeMinutes => App.UpTimeList.FirstOrDefault(u => u.Date == DateOnly.FromDateTime(DateTime.Now)) != null
		? (uint)App.UpTimeList.FirstOrDefault(u => u.Date == DateOnly.FromDateTime(DateTime.Now))!.Minutes % 60
		: 0;


	private readonly IDataIssuer _dataIssuer;
	private readonly ICustomDialogService _customDialog;
	private readonly IRetrieveChartService _retrieveChartService;


	// Reassign tracked App to new values.
	public Task TrackedAppItemVM_Director_WorkDone(object arg1, int arg2)
	{
		Log.Information("{@Method} - Get data for ({@app}).", nameof(TrackedAppItemVM_Director_WorkDone), App.Name);
		App = _dataIssuer.GetAppDataByName(App.Name) ?? App;

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


		//AppTodayTimeHours = (uint)App.UpTimeList.FirstOrDefault(u => u.Date == DateOnly.FromDateTime(DateTime.Now))?.Minutes / 60;



		_seriesCollection = _retrieveChartService.GetSeriesForAppLastWeek(_app);
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
