using LiveCharts.Wpf;
using Serilog;
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
using UI.WPF.ViewModels;

namespace UI.WPF.Components
{
	/// <summary>
	/// Interaction logic for FullHistoryTrackedAppView.xaml
	/// </summary>
	public partial class FullHistoryTrackedAppView : System.Windows.Controls.UserControl
    {
		public FullHistoryTrackedAppView()
		{
			InitializeComponent();
		}

		private bool limitMin = false, limitMax = false;	

		private void Axis_RangeChanged(LiveCharts.Events.RangeChangedEventArgs eventArgs)
		{
			//var vm = (FullHistoryTrackedAppViewModel)DataContext;

			//var currentRange = eventArgs.Range;

			//if (currentRange < TimeSpan.TicksPerDay * 2)
			//{
			//	vm.Formatter = x => new DateTime((long)x).ToString("t");
			//	return;
			//}

			//if (currentRange < TimeSpan.TicksPerDay * 60)
			//{
			//	vm.Formatter = x => new DateTime((long)x).ToString("dd MMM yy");
			//	return;
			//}

			//if (currentRange < TimeSpan.TicksPerDay * 540)
			//{
			//	vm.Formatter = x => new DateTime((long)x).ToString("MMM yy");
			//	return;
			//}

			//vm.Formatter = x => new DateTime((long)x).ToString("MMM yy");

			Axis ax = (Axis)eventArgs.Axis;
			var vm = (FullHistoryTrackedAppViewModel)DataContext;

			if(limitMax)
			{
				ax.MaxValue = vm.AxisXMaxValue;
			}

			if(limitMin)
			{
				ax.MinValue = vm.AxisXMinValue;
			}

			Log.Warning("VM MinValue ({@min}), MaxValue ({@max}).", vm.AxisXMinValue, vm.AxisXMaxValue);
			Log.Warning("Axis MinValue ({@min}), MaxValue ({@max}).", ax.MinValue, ax.MaxValue);
		}

		private void X_PreviewRangeChanged(LiveCharts.Events.PreviewRangeChangedEventArgs eventArgs)
		{
			var vm = (FullHistoryTrackedAppViewModel)DataContext;

			if(eventArgs.PreviewMinValue < -0.5)
			{
				eventArgs.Cancel = true;
			}

			if(eventArgs.PreviewMaxValue > vm.Labels?.Count() + 0.5)
			{
				eventArgs.Cancel = true;
			}

			limitMax = eventArgs.PreviewMaxValue > vm.Labels?.Count() + 0.5;
			limitMin = eventArgs.PreviewMinValue < -0.5;
		}
    }
}
