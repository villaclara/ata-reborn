using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LiveCharts;
using System.Globalization;
using UI.WPF.Services;
using UI.WPF.Services.Abstracts;

namespace UI.WPF.ViewModels;

public partial class FullHistoryTrackedAppViewModel : BaseViewModel, IRecipient<MessageApp>
{
	private readonly IRetrieveChartService _retrieveChart;


	[ObservableProperty]
	private IChartValues _chartValues;

	[ObservableProperty]
	private string[]? _labels;

	[ObservableProperty]
	private Func<double, string>? _yFormatter;

	[ObservableProperty]
	private double _axisXMinValue;

	[ObservableProperty]
	private double _axisXMaxValue;


	[ObservableProperty]
	private string _appName = "bruh";

	[ObservableProperty]
	private double _totalTimeMins = 0;

	[ObservableProperty]
	private double _totalTimeHours = 0;

	[ObservableProperty]
	private double _maxTimeTime = 0;

	[ObservableProperty]
	private string _maxTimeDate = "";

	[ObservableProperty]
	private string _firstSessionDate = "";

	public FullHistoryTrackedAppViewModel(IRetrieveChartService retrieveChart)
	{
		_retrieveChart = retrieveChart;
		ChartValues = new ChartValues<double>();
		YFormatter = value => value.ToString("N2"); // value to be displayed as ("1.00") in Axis Y

		StrongReferenceMessenger.Default.Register<MessageApp>(this);
	}

	public void Receive(MessageApp message)
	{
		var app = message.appVM;

		// Assign App Details
		AppName = app.Name;
		TotalTimeMins = Math.Round(app.UpTimeList.Sum(u => u.Minutes), 2);
		TotalTimeHours = Math.Round(TotalTimeMins / 60, 2);
		MaxTimeDate = app.UpTimeList.MaxBy(u => u.Minutes)?.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) ?? "Not set yet";
		MaxTimeTime = app.UpTimeList.MaxBy(u => u.Minutes)?.Minutes ?? 0;
		FirstSessionDate = app.UpTimeList.FirstOrDefault()?.Date.ToString("dd MMM yyyy", CultureInfo.InvariantCulture) ?? "Not set yet";

		// Get Labels and ChartValues
		Labels = _retrieveChart.GetLabelsForAllTime(app);
		ChartValues = _retrieveChart.GetChartValuesForAllTime(app);

		// Set Values for first display of Chart
		AxisXMinValue = 0;
		AxisXMaxValue = Labels.Length - 1 < 10 ? Labels.Length : 10;        // Set the MaxValue to 10 -- Show only 10 chart values at start by default

	}

}
