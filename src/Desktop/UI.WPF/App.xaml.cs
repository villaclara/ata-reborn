using Application.Common.Services;
using Application.Director.Creation;
using Application.Director.Instance;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
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
		AppHost = Host.CreateDefaultBuilder()
			.ConfigureServices(services =>
			{
				services.AddSingleton<IDirector>(new DirectorBuilder()
					.AddIOServices(new ReadDataFromJsonFile(), new WriteDataStringToFile())
					.SetWritableFile("apps.json")
					.SetTimerCheckValue(5000)
					.Build());
				
				services.AddSingleton<MainWindow>();


				services.AddSingleton<ToolbarViewModel>();
			})
			.Build();
	}

	protected override void OnStartup(StartupEventArgs e)
	{

		AppHost.Start();

		MainWindow = AppHost.Services.GetRequiredService<MainWindow>();
		MainWindow.Show();

		base.OnStartup(e);
	}

}
