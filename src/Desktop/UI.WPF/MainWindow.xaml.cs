using Application.Common.Services;
using Application.Director.Creation;
using Serilog;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.WPF.Components;
using UI.WPF.ViewModels;

namespace UI.WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{


		public MainWindow()
		{
			InitializeComponent();

			//this.DataContext = new MainWindowViewModel();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			
		}

		private Task Director_WorkDone(object arg1, int arg2)
		{
			return Task.CompletedTask;
		}
	}
}