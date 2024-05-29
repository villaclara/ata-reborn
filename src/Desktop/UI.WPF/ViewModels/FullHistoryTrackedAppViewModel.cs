﻿using CommunityToolkit.Mvvm.ComponentModel;
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
using System.Globalization;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.Services;
using UI.WPF.Services.Abstracts;
using UI.WPF.Utilities;

namespace UI.WPF.ViewModels;


// TO DO
//
// If the app was just added then exception is thrown when trying to get the Chart for it
// ex - One axis has an invalid range, it is or it is tends to zero, please ensure your axis has a valid range'

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
		YFormatter = value => value.ToString("N2");	// value to be displayed as ("1.00") in Axis Y

		StrongReferenceMessenger.Default.Register<MessageApp>(this);
	}

	public void Receive(MessageApp message)
	{
		var app = message.appVM;

		// Assign App Details
		AppName = app.Name;
		TotalTimeMins = app.UpTimeList.Sum(u => u.Minutes);
		TotalTimeHours = Math.Round(TotalTimeMins / 60, 2);
		MaxTimeDate = app.UpTimeList.MaxBy(u => u.Minutes)!.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
		MaxTimeTime = app.UpTimeList.MaxBy(u => u.Minutes)!.Minutes;
		FirstSessionDate = app.UpTimeList.First().Date.ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
	
		// Get Labels and ChartValues
		Labels = _retrieveChart.GetLabelsForAllTime(app);
		ChartValues = _retrieveChart.GetChartValuesForAllTime(app);

		// Set Values for first display of Chart
		AxisXMinValue = 0;
		AxisXMaxValue = Labels.Length - 1 < 10 ? Labels.Length - 1 : 10;		// Set the MaxValue to 10 -- Show only 10 chart values at start by default

	}

}
