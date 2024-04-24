using Application.Common.Abstracts;
using Application.Common.Services;
using Application.Director.Creation;
using Application.Director.Instance;
using Application.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using Serilog;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using UI.WPF.Services.Abstracts;
using UI.WPF.Services.Implementations;
using UI.WPF.ViewModels;
using UI.WPF.Views;
using Forms = System.Windows.Forms;

namespace UI.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
	public IHost AppHost { get; private set; }

	private readonly Forms.NotifyIcon _notifyIcon;

	public App()
	{
		InitializeComponent();

		// Clear the log file. 
		// 
		// Maybe it is worth to move it somewhere else, idk
		File.WriteAllText(ConstantValues.LOG_FILE_NAME, "");

		_notifyIcon = new Forms.NotifyIcon();

		AppHost = Host.CreateDefaultBuilder()
			.ConfigureServices(services =>
			{

				Log.Logger = new LoggerConfiguration()
					.WriteTo.File(path: ConstantValues.LOG_FILE_NAME, fileSizeLimitBytes: 10_000_000, rollOnFileSizeLimit: true, retainedFileCountLimit: 3, shared: true,
									restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
					.CreateLogger();
				Log.Logger.Information("{@Method} - start serilog in WPF.", nameof(App));
				Log.Logger.Information("{@Method} - WPF version - {@wpf}.", nameof(App), Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "null");

				services.AddSingleton<IDirector>(new DirectorBuilder()
					.AddIOServices(new ReadDataFromJsonFile(), new WriteDataStringToFile())
					.SetWritableFile("apps.json")
					.SetTimerCheckValue(18000)
					.Build());

				// Add Services
				services.AddSingleton<INavigationService, NavigationService>();
				services.AddSingleton<IGetProcs, GetProcsService>();
				services.AddSingleton<IThemeChangeService, ThemeChangeService>();
				services.AddSingleton<IRetrieveChartService, RetrieveChartService>();
				services.AddSingleton<IConfigService, XMLConfigService>();

				services.AddTransient<ICustomDialogService, CustomDialogService>();



				// Add ViewModels 
				services.AddSingleton<TrackedAppsViewModel>();
				services.AddSingleton<ToolbarViewModel>();
				services.AddSingleton<TopRowViewModel>();
				services.AddTransient<SettingsViewModel>();

				// Transient as we want to retrieve new Processes list every time we reach this control. 
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

		_notifyIcon.Icon = new Icon(System.Windows.Application.GetResourceStream(new Uri("/Resources/Images/atav2.ico", UriKind.Relative)).Stream);
		_notifyIcon.Visible = true;
		_notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
		_notifyIcon.Click += NotifyIcon_DoubleClick;
		_notifyIcon.Text = "ATA Reborn. Double click to open.";

#if DEBUG

#else

		RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)!;
		reg.SetValue("ATAREBORN", Process.GetCurrentProcess().MainModule!.FileName.ToString());

#endif

		base.OnStartup(e);
	}

	private void NotifyIcon_DoubleClick(object? sender, EventArgs e)
	{
		MainWindow.Show();
		MainWindow.WindowState = WindowState.Normal;
	}

	protected override void OnExit(ExitEventArgs e)
	{
		_notifyIcon.Dispose();

		base.OnExit(e);
	}

}
