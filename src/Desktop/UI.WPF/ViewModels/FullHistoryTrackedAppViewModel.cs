using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
	private Func<double, string>? _xFormatter;


	[ObservableProperty]
	private double _axisXMinValue;

	[ObservableProperty]
	private double _axisXMaxValue;


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


		XFormatter = value => new DateTime((long)value).ToString("dd/MM");
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

		
		
		//
		//
		//////////////////////////////////////// USING default ChartVAlues and Labels
		Labels = _retrieveChart.GetLabelsForAllTime(app);
		ChartValues = _retrieveChart.GetChartValuesForAllTime(app);


		AxisXMinValue = 0;
		AxisXMaxValue = Labels.Length - 1 < 10 ? Labels.Length - 1 : 10;		// Set the MaxValue to 10 -- Show only 10 chart values at start by default




		//
		////////////////////////////////////// USING DateTimePoint
		//
		//
		//List<DateTimePoint> points = new List<DateTimePoint>();

		//foreach(var d in app.CreatedAt.GetDatesOnlyRangeFromDateToToday())
		//{
		//	points.Add(new DateTimePoint(
		//		new DateTime(d, TimeOnly.MinValue), app.UpTimeList.FirstOrDefault(x => x.Date == d)?.Minutes ?? 0)
		//		);
		//}

		//ChartValues = points.AsChartValues();

		//AxisXMinValue = app.CreatedAt.Ticks;
		//AxisXMaxValue = DateTime.Today.Ticks;
	}


}
