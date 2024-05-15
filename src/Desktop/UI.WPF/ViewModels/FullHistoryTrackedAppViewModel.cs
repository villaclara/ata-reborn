using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LiveCharts;
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
	private AppInstanceVM _app;
	private readonly IRetrieveChartService _retrieveChart;
	[ObservableProperty]
	private SeriesCollection? _seriesCollection;

	[ObservableProperty]
	private string[]? _labels;

	[ObservableProperty]
	private Func<double, string>? _formatter;

	[ObservableProperty]
	private string _appName = "bruh";

	public FullHistoryTrackedAppViewModel(IRetrieveChartService retrieveChart)
	{
		_retrieveChart = retrieveChart;

		

		StrongReferenceMessenger.Default.Register<MessageApp>(this);

	}

	public void Receive(MessageApp message)
	{
		AppName = message.appVM.Name;

		SeriesCollection = _retrieveChart.GetSeriesForAppLastWeek(_app);
		Labels = _retrieveChart.GetLabelsLastWeek();
		Formatter = value => value.ToString("N");
	}
}
