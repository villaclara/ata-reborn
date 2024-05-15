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
}
