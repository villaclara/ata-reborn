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


		// Event for Preventing the Chart to be scrolled Outside of array on X Axis
		private void X_PreviewRangeChanged(LiveCharts.Events.PreviewRangeChangedEventArgs eventArgs)
		{
			var vm = (FullHistoryTrackedAppViewModel)DataContext;

			if(eventArgs.PreviewMinValue < -0.5)
			{
				eventArgs.Cancel = true;
			}

			if(eventArgs.PreviewMaxValue > vm.Labels?.Length + 0.5)
			{
				eventArgs.Cancel = true;
			}

		}
    }
}
