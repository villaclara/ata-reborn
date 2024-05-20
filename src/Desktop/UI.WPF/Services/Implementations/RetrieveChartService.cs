﻿using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Serilog;
using Shared.ViewModels;
using UI.WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.Services.Abstracts;

namespace UI.WPF.Services.Implementations;

public class RetrieveChartService : IRetrieveChartService
{
	public ColumnSeries GetColumnSeriesForAllTime(AppInstanceVM app)
	{
		if(app is null)
		{
			return new ColumnSeries()
			{
				Title = "Times",
				Values = new ChartValues<double>()
			};
		}

		Log.Information("{@Method} - Get ColumnSeries for all time for ({@app}).", nameof(GetColumnSeriesForAllTime), app.Name);
		DateTime firstSessionDate = app.CreatedAt;
		IEnumerable<DateOnly> dates = firstSessionDate.GetDatesOnlyRangeFromDateToToday();
		Log.Information("{@Method} - Dates from CreatedAt count ({@count}).", nameof(GetColumnSeriesForAllTime), dates.Count());
		
		ChartValues<double> chartValues = [];
		foreach (var date in dates)
		{
			chartValues.Add(app.UpTimeList.Where(u => u.Date == date).FirstOrDefault()?.Minutes ?? 0);
		}
		Log.Information("{@Method} - ColumnSeries after adding count ({@count}).", nameof(GetColumnSeriesForAllTime), chartValues.Count);
		
		return new ColumnSeries()
		{
			Title = "Time",
			Values = chartValues
		};
	}

	public string[] GetLabelsForAllTime(AppInstanceVM app)
	{
		if(app is null)
		{
			return [];
		}

		Log.Information("{@Method} - Get Labels for all time for ({@app}).", nameof(GetLabelsForAllTime), app.Name);
		DateTime firstSessionDate = app.CreatedAt;
		Log.Information("CreatedAtDate - {@fsd}.", firstSessionDate);

		List<DateTime> allDatesFromFirst = [];
		IEnumerable<DateOnly> dates = firstSessionDate.GetDatesOnlyRangeFromDateToToday();
		Log.Information("{@Method} - Labels count ({@count) for app ({@app}).", nameof(GetLabelsForAllTime), dates.Count(), app.Name);
		
		return dates.Select(d => d.ToString("dd/MM")).ToArray();
	}

	public string[] GetLabelsLastWeek()
	{
		Log.Information("{@Method} - Get labels.", nameof(GetLabelsLastWeek));
		return [
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-7)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-6)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-5)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-4)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-3)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-2)):dd/MM}",
			$"{DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1)):dd/MM}"
		];
	}

	public SeriesCollection GetSeriesForAppLastWeek(AppInstanceVM app)
	{
		if (app is null)
		{
			return [];
		}

		Log.Information("{@Method} - Get series for App({@app}).", nameof(GetSeriesForAppLastWeek), app.Name);
		var series = new SeriesCollection
		{
			new ColumnSeries
			{
				Title = "Time",
				Values = new ChartValues<double> {
					app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-7))).FirstOrDefault()?.Minutes ?? 0,
					app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-6))).FirstOrDefault()?.Minutes ?? 0,
					app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-5))).FirstOrDefault()?.Minutes ?? 0,
					app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-4))).FirstOrDefault()?.Minutes ?? 0,
					app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-3))).FirstOrDefault()?.Minutes ?? 0,
					app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-2))).FirstOrDefault()?.Minutes ?? 0,
					app.UpTimeList.Where(u => u.Date == DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1))).FirstOrDefault()?.Minutes ?? 0
				},

			}
		};

		Log.Information("series - {@series}.", series.Count);
		return series;
	}

}
