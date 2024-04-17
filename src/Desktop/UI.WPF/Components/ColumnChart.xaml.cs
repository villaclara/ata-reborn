using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Controls;

namespace UI.WPF.Components;

/// <summary>
/// Interaction logic for ColumnChart.xaml
/// </summary>
public partial class ColumnChart : UserControl
{
	public ColumnChart()
	{
		InitializeComponent();

		SeriesCollection = new SeriesCollection()
		{
			new ColumnSeries
			{
				Title = "M",
				Values = new ChartValues<double> { 10, 20, 30 }
			}
		};

		//also adding values updates and animates the chart automatically
		SeriesCollection[1].Values.Add(48d);

		Labels = new[] { "Maria", "Susan", "Charles", "Frida" };
		Formatter = value => value.ToString("N");

		DataContext = this;
	}

	public SeriesCollection SeriesCollection { get; set; }
	public string[] Labels { get; set; }
	public Func<double, string> Formatter { get; set; }


}
