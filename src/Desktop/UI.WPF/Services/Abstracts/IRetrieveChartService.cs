using LiveCharts;
using LiveCharts.Wpf;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.Services.Abstracts;

/// <summary>
/// Service to get the Axis X, Axis Y and values for LiveCharts.
/// </summary>
public interface IRetrieveChartService
{
	/// <summary>
	/// Get <see cref="SeriesCollection"/>, the Axis Y and Values.
	/// </summary>
	/// <param name="app"><see cref="AppInstanceVM"/> object to get values.</param>
	/// <returns><see cref="SeriesCollection"/> object.</returns>
	SeriesCollection GetSeriesForAppLastWeek(AppInstanceVM app);
	
	/// <summary>
	/// Retrieve labels for the chart. Should be used with <see cref="GetSeriesForAppLastWeek(AppInstanceVM)"/>.
	/// </summary>
	/// <returns>Array of <see cref="string"/> representing values of Axis X.</returns>
	string[] GetLabelsLastWeek();

	/// <summary>
	/// Get the <see cref="ColumnSeries"/> instance (that is what you should add inside <see cref="SeriesCollection"/>) of the app for full time.
	/// </summary>
	/// <param name="app"><see cref="AppInstanceVM"/> object to get values.</param>
	/// <returns><see cref="ColumnSeries"/> collection.</returns>
	ColumnSeries GetColumnSeriesForAllTime(AppInstanceVM app);

	/// <summary>
	/// Get the labels for the chart for full app time.
	/// </summary>
	/// <param name="app"><see cref="AppInstanceVM"/> object to get values.</param>
	/// <returns><see cref="string"/> array of <see cref="DateOnly"/> objects starting with App First Session Date.</returns>
	string[] GetLabelsForAllTime(AppInstanceVM app);
}
