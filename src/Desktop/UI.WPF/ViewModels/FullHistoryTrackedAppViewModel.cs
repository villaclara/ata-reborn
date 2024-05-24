using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Charts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using Shared.Models;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.Services;
using UI.WPF.Services.Abstracts;
using UI.WPF.Utilities;

namespace UI.WPF.ViewModels;

public partial class FullHistoryTrackedAppViewModel : BaseViewModel, IRecipient<MessageApp>
{
	private readonly IRetrieveChartService _retrieveChart;
	
	[ObservableProperty]
	private SeriesCollection _seriesCollection;

	[ObservableProperty]
	private string[]? _labels;

	[ObservableProperty]
	private Func<double, string>? _formatter = value => value + "sssss";


	[ObservableProperty]
	private Func<DateOnly, string>? _formatterDateOnly = value => value.ToString("dd/MM");

	[ObservableProperty]
	private DateTime _minValue;

	[ObservableProperty]
	private DateTime _maxValue;


	[ObservableProperty]
	private DateOnly[] _dateOnlyLabels;


	[ObservableProperty]
	private IChartValues _chartValues;

	[ObservableProperty]
	private string _appName = "bruh";

	public FullHistoryTrackedAppViewModel(IRetrieveChartService retrieveChart)
	{
		_retrieveChart = retrieveChart;
		SeriesCollection = [];


		ChartValues = new ChartValues<double>();

		StrongReferenceMessenger.Default.Register<MessageApp>(this);


		Formatter = value => value.ToString("dd/MM");
	}

	public void Receive(MessageApp message)
	{
		AppName = message.appVM.Name;
		var app = message.appVM;

		//SeriesCollection.Add(new LineSeries()
		//{
		//	Title = "Times",
		//	Values = _retrieveChart.GetChartValuesForAllTime(app)
		//});
		//Labels = _retrieveChart.GetLabelsForAllTime(app);

		ChartValues = _retrieveChart.GetChartValuesForAllTime(app);





		MinValue = new DateTime(message.appVM.UpTimeList.First().Date, TimeOnly.MinValue);
		MaxValue = new DateTime(message.appVM.UpTimeList.Last().Date, TimeOnly.MinValue);
	}
}
