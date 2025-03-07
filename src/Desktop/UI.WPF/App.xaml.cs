﻿using System.IO;
using System.Reflection;
using System.Windows;
using Application.Common.Abstracts;
using Application.Common.Services;
using Application.Director.Creation;
using Application.Director.Instance;
using Application.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
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
				//services.AddSingleton<IConfigService, XMLConfigService>();
				services.AddSingleton<IConfigService, JSONConfigService>();
				services.AddSingleton<IChangelogService, ChangelogService>();

				services.AddTransient<ICustomDialogService, CustomDialogService>();

				services.AddSingleton<IWindowCreator, WhatsNewWindowCreator>();

				// Add ViewModels 
				services.AddSingleton<TrackedAppsViewModel>();
				services.AddSingleton<TrackedAppsViewModel_Minimal>();
				services.AddSingleton<ToolbarViewModel>();
				services.AddSingleton<TopRowViewModel>();
				services.AddSingleton<SettingsViewModel>();
				services.AddSingleton<ChangelogPageViewModel>();
				// Singleton FullHistory View Model as we get the App trakced in Receive method and just redraw the Chart
				services.AddSingleton<FullHistoryTrackedAppViewModel>();

				// Transient as we want to retrieve new Processes list every time we reach this control. 
				services.AddTransient<ProcessListViewModel>();

				services.AddSingleton<WhatsNewViewModel>();
				services.AddSingleton<ContextMenuStripViewModel>();

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

#if DEBUG
		System.Windows.MessageBox.Show("debug");
#endif


		// director first to initialize it. Initialization is performed at RunAsync()
		var director = AppHost.Services.GetRequiredService<IDirector>();
		director.RunAsync();

		AppHost.Start();

		MainWindow = AppHost.Services.GetRequiredService<MainWindow>();

		// check whether we want to display MainWindow or start in system tray.
		var configService = AppHost.Services.GetRequiredService<IConfigService>();
		var sMinimized = configService.GetBooleanValue("StartMinimized");

		if (!sMinimized)
		{
			MainWindow.Show();
		}

		_notifyIcon.Icon = new Icon(System.Windows.Application.GetResourceStream(new Uri("/Resources/Images/atav2.ico", UriKind.Relative)).Stream);
		_notifyIcon.Visible = true;
		_notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
		_notifyIcon.MouseDown += NotifyIcon_MouseDown;

		_notifyIcon.ContextMenuStrip = new();

#if DEBUG
		_notifyIcon.Text = "(DEBUG) ATA Reborn. Double click to open.";

#else
		_notifyIcon.Text = "ATA Reborn. Double click to open.";
#endif

		base.OnStartup(e);
	}

	private void NotifyIcon_MouseDown(object? sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right)
		{
			var notify = sender as NotifyIcon;
			if (notify == null)
			{
				return;
			}

			if (notify.ContextMenuStrip is null)
			{
				notify.ContextMenuStrip = new();
			}

			notify.ContextMenuStrip.Items.Clear();
			var nameToolStrip = new ToolStripMenuItem("ATA Reborn");
			nameToolStrip.Click -= NotifyIcon_DoubleClick;
			nameToolStrip.Click += NotifyIcon_DoubleClick;
			notify.ContextMenuStrip.Items.Add(nameToolStrip);
			notify.ContextMenuStrip.Items.Add(new ToolStripSeparator());

			ToolStripMenuItem appsItem = new ToolStripMenuItem("Apps");
			var dir = AppHost.Services.GetRequiredService<IDirector>();
			foreach (var app in dir.Apps)
			{
				appsItem.DropDownItems.Add($"{app.Name} - {app.CurrentSessionTime} m");
			}
			notify.ContextMenuStrip.Items.Add(appsItem);

			notify.ContextMenuStrip.Items.Add(new ToolStripSeparator());

			var exitToolStrip = new ToolStripMenuItem("Quit");
			exitToolStrip.Click -= ExitToolStrip_Click;
			exitToolStrip.Click += ExitToolStrip_Click;
			notify.ContextMenuStrip.Items.Add(exitToolStrip);
		}
	}

	private void ExitToolStrip_Click(object? sender, EventArgs e)
	{
		Current.Shutdown();
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
