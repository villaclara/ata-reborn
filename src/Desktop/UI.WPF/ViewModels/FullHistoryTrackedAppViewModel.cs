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
		SeriesCollection = [];
		

		StrongReferenceMessenger.Default.Register<MessageApp>(this);

	}

	public void Receive(MessageApp message)
	{
		AppName = message.appVM.Name;
		var app = message.appVM;
		//SeriesCollection = _retrieveChart.GetSeriesForAppLastWeek(_app);

		SeriesCollection.Clear();
		SeriesCollection.Add(new ColumnSeries
		{
			Title = "Time",
			Values = new ChartValues<ObservableValue> {
					new ObservableValue() { Value =
						 app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-7))).FirstOrDefault()?.Minutes ?? 0,
					},
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
						app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1))).FirstOrDefault()?.Minutes ?? 0
					}
			}
		}

		);
		Labels = _retrieveChart.GetLabelsLastWeek();
		Formatter = value => value.ToString("N");
	}
}
