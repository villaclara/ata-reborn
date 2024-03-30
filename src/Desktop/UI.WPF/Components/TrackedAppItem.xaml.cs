using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.WPF.Components;

/// <summary>
/// Interaction logic for TrackedAppItem.xaml
/// </summary>
public partial class TrackedAppItem : UserControl
{
	public TrackedAppItem()
	{
		InitializeComponent();
		SeriesCollection = new SeriesCollection()
		{
			new ColumnSeries
			{
				Title = "Time",
				Values = new ChartValues<double> { 10, 20, 30, 0, 20, 1, 0 }
			}
		};


		Labels = new[] { "23/03", "24/03", "25/03", "26/03", "27/03", "28/03", "29/03" };
		Formatter = value => value.ToString("N");

		DataContext = this;
	}

	public SeriesCollection SeriesCollection { get; set; }
	public string[] Labels { get; set; }
	public Func<double, string> Formatter { get; set; }
}
