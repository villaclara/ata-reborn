using Application.Common.Abstracts;
using Application.Common.Services;
using Application.Director.Creation;
using Application.Director.Instance;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using UI.WPF.Services;
using UI.WPF.ViewModels;

namespace UI.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
	public IHost AppHost { get; private set; }


	public App()
	{
		InitializeComponent();

		// Clear the log file. 
		// 
		// Maybe it is worth to move it somewhere else, idk
		File.WriteAllText("log.txt", "");

		AppHost = Host.CreateDefaultBuilder()
			.ConfigureServices(services =>
			{

				Log.Logger = new LoggerConfiguration()
					.WriteTo.File("log.txt", fileSizeLimitBytes: 10_000_000, rollOnFileSizeLimit: true, retainedFileCountLimit: 3, shared: true,
									restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
					.CreateLogger();
				Log.Logger.Information("{@Method} - start serilog in WPF.", nameof(App));

				services.AddSingleton<IDirector>(new DirectorBuilder()
					.AddIOServices(new ReadDataFromJsonFile(), new WriteDataStringToFile())
					.SetWritableFile("apps.json")
					.SetTimerCheckValue(18000)
					.Build());

				// Different Services, not ViewModels
				services.AddSingleton<INavigationService, NavigationService>();
				services.AddSingleton<IGetProcs, GetProcsService>();


				services.AddSingleton<TrackedAppsViewModel>();
				services.AddSingleton<ToolbarViewModel>();

				// Transient as we want to retrieve new Processes list every time we reach this control. 
				// Now the Process List is retrieved in Constructor. 
				services.AddTransient<ProcessListViewModel>();

				services.AddSingleton<MainWindowViewModel>();
				// Gets the Required ViewModel by Navigation
				services.AddSingleton<Func<Type, BaseViewModel>>(sp =>
					viewModelType => (BaseViewModel)sp.GetRequiredService(viewModelType));



				// Add MainWindow and set the DataContext here
				services.AddSingleton<MainWindow>(sp => new MainWindow()
				{
					DataContext = sp.GetRequiredService<MainWindowViewModel>()
				});

			})
			.Build();
	}

	protected override void OnStartup(StartupEventArgs e)
	{
		// director first to initialize it. Initialization is performed at RunAsync()
		var director = AppHost.Services.GetRequiredService<IDirector>();
		director.RunAsync();

		AppHost.Start();

		MainWindow = AppHost.Services.GetRequiredService<MainWindow>();
		MainWindow.Show();

		base.OnStartup(e);
	}

}
