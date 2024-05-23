using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.Services;
using UI.WPF.Services.Abstracts;

namespace UI.WPF.ViewModels;

public partial class FullHistoryTrackedAppViewModel : BaseViewModel, IRecipient<MessageApp>
{
	private readonly IRetrieveChartService _retrieveChart;
	
	[ObservableProperty]
	private SeriesCollection _seriesCollection;

	[ObservableProperty]
	private string[]? _labels;

	[ObservableProperty]
	private Func<double, string>? _formatter;

	[ObservableProperty]
	private SeriesCollection _seriesCollection1;

	[ObservableProperty]
	private string[]? _labels1;

	[ObservableProperty]
	private Func<double, string>? _formatter1;


	[ObservableProperty]
	private string _appName = "bruh";

	public FullHistoryTrackedAppViewModel(IRetrieveChartService retrieveChart)
	{
		_retrieveChart = retrieveChart;
		SeriesCollection = [];

		SeriesCollection1 = [];

		StrongReferenceMessenger.Default.Register<MessageApp>(this);

	}

	public void Receive(MessageApp message)
	{
		AppName = message.appVM.Name;
		var app = message.appVM;

		//SeriesCollection.Clear();
		SeriesCollection.Add(new LineSeries()
		{
			Title = "Times",
			Values = _retrieveChart.GetChartValuesForAllTime(app)
		});
		Labels = _retrieveChart.GetLabelsForAllTime(app);

		//SeriesCollection1.Clear();
		SeriesCollection1.Add(new ColumnSeries()
		{
			Title = "Times",
			Values = _retrieveChart.GetChartValuesForAllTime(app)
		});
		Labels1 = _retrieveChart.GetLabelsForAllTime(app);


		StrongReferenceMessenger.Default.Unregister<MessageApp>(this);
	}
}
