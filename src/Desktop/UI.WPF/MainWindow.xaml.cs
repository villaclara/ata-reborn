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
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			File.WriteAllText("log.txt", "");
			
			Log.Logger = new LoggerConfiguration()
				.WriteTo.Console()
				.WriteTo.File("log.txt")
				.CreateLogger();


			Log.Information("{@Method} - start.", nameof(Window_Loaded));

			var director = new DirectorBuilder()
				.AddIOServices(new ReadDataFromJsonFile(), new WriteDataStringToFile())
				.SetWritableFile("apps.json")
				.SetTimerCheckValue(5000)
				.Build();

			director.WorkDone += Director_WorkDone;

			await director.RunAsync();

			foreach(var app in director.Apps)
			{
				TrackedAppItemViewModel vm = new TrackedAppItemViewModel(app);
				director.WorkDone += vm.Director_WorkDone;
				TrackedAppItem item = new TrackedAppItem();
				item.DataContext = vm;
				item.Width = 300;
				WrapPanelMain.Children.Add(item);
			}
		}

		private Task Director_WorkDone(object arg1, int arg2)
		{
			return Task.CompletedTask;
		}
	}
}