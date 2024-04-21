using LiveCharts;
using LiveCharts.Wpf;
using Serilog;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.Services.Abstracts;

namespace UI.WPF.Services.Implementations;

public class RetrieveChartService : IRetrieveChartService
{
	public string[] GetLabels()
	{
		Log.Information("{@Method} - Get labels.", nameof(GetLabels));
		return [ 
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-6)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-5)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-4)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-3)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-2)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now):dd/MM}"
		];
	}

	public SeriesCollection GetSeriesForApp(AppInstanceVM app)
	{
		Log.Information("{@Method} - Get series for App({@app}).", nameof(GetSeriesForApp), app.Name);
		var series = new SeriesCollection
		{
			new ColumnSeries
			{
				Title = "Time",
				Values = new ChartValues<double> {
					app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-6))).FirstOrDefault()?.Minutes ?? 0,
					app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-5))).FirstOrDefault()?.Minutes ?? 0,
					app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-4))).FirstOrDefault()?.Minutes ?? 0,
					app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-3))).FirstOrDefault()?.Minutes ?? 0,
					app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-2))).FirstOrDefault()?.Minutes ?? 0,
					app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1))).FirstOrDefault()?.Minutes ?? 0,
					app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now)).FirstOrDefault()?.Minutes ?? 0
				}
			}
		};

		Log.Information("series - {@series}.", series.Count);
		return series;
	}

}
